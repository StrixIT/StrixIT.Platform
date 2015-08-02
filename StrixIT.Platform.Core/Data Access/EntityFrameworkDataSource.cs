#region Apache License

//-----------------------------------------------------------------------
// <copyright file="EntityFrameworkDataSource.cs" company="StrixIT">
// Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

#endregion Apache License

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The IDataSource implementation for the Entity Framework.
    /// </summary>
    public abstract class EntityFrameworkDataSource : DbContext, IDataSource
    {
        #region Private Fields

        /// <summary>
        /// A store to cache all key property names for entity types.
        /// </summary>
        private static ConcurrentDictionary<Type, string[]> _keyPropertyNames = new ConcurrentDictionary<Type, string[]>();

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkDataSource"/> class.
        /// </summary>
        /// <param name="connectionStringName">
        /// The name of the connection string to use. This name can be located in any of the
        /// configuration files loaded by the <see cref="ConfigurationLoader"/> class
        /// </param>
        protected EntityFrameworkDataSource(string connectionStringName) : base(GetConnection(connectionStringName))
        {
        }

        #endregion Protected Constructors

        #region Query

        /// <summary>
        /// Gets a query for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The query</returns>
        public IQueryable<T> Query<T>() where T : class
        {
            var query = this.Set<T>();
            this.SecureQuery(query);
            return query;
        }

        #endregion Query

        #region Save

        /// <summary>
        /// Saves one or more entities to the data source. To persist them, SaveChanges should be
        /// called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to save</typeparam>
        /// <param name="entity">The entity or entities to save</param>
        /// <returns>The saved entity or entities</returns>
        public T Save<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!entity.Validate())
            {
                throw new ValidationException();
            }

            // Allow for deleting either one entity or a collection of entities. Create a list if
            // only one is passed, and create the lists to hold the entities that need to be added
            // and the result entities.
            Type entityType = ObjectContext.GetObjectType(entity.GetType());
            IEnumerable list;
            IEnumerable resultList;
            MethodInfo addMethod;

            if (!typeof(IEnumerable).IsAssignableFrom(entityType))
            {
                list = Helpers.CreateGenericList(entityType, 1);
                resultList = Helpers.CreateGenericList(entityType, 1);
                addMethod = list.GetType().GetMethod("Add");
                addMethod.Invoke(list, new object[] { entity });
            }
            else
            {
                list = entity as IEnumerable;
                resultList = Helpers.CreateGenericList(entityType.GetGenericArguments().First(), list.Length());
                addMethod = list.GetType().GetMethod("Add");
                entityType = entityType.GetGenericArguments().First();
            }

            // Check whether the entities can be accessed
            if (this.CheckEntityAccessAllowed(entityType, this.GetKeyValues(entityType, list)))
            {
                foreach (var item in list)
                {
                    var entry = this.Entry(item);
                    bool isAttached = entry.State != EntityState.Detached;

                    // If a tracked, deleted object is saved, just remove the deleted status.
                    if (isAttached)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            entry.State = EntityState.Modified;
                        }

                        addMethod.Invoke(resultList, new object[] { item });
                        continue;
                    }

                    // Try to find the item in memory, else get it from the database. This cannot be
                    // done more efficiently, as a find is required for each object.
                    var existingEntity = this.GetExistingObject(item);

                    if (existingEntity == null)
                    {
                        this.SetGuidKey(entityType, item);
                        this.Set(entityType).Add(item);
                        addMethod.Invoke(resultList, new object[] { item });
                        continue;
                    }

                    var existingEntry = this.Entry(existingEntity);

                    // Remove the deleted status if the existing entry is marked as deleted.
                    if (existingEntry.State == EntityState.Deleted)
                    {
                        existingEntry.State = EntityState.Modified;
                    }

                    // Update the values of the existing entity.
                    existingEntry.CurrentValues.SetValues(item);
                    addMethod.Invoke(resultList, new object[] { item });
                }

                if (resultList.Length() == 1)
                {
                    return resultList.GetFirst() as T;
                }
                else
                {
                    return resultList as T;
                }
            }

            return null;
        }

        #endregion Save

        #region Delete

        /// <summary>
        /// Deletes one or more entities from the data source. To persist the delete, SaveChanges
        /// should be called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to delete</typeparam>
        /// <param name="entity">The entity or entities to delete</param>
        public void Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Type entityType = ObjectContext.GetObjectType(entity.GetType());
            IEnumerable list;

            if (!typeof(IEnumerable).IsAssignableFrom(entityType))
            {
                list = Helpers.CreateGenericList(entityType, 1);
                list.GetType().GetMethod("Add").Invoke(list, new object[] { entity });
            }
            else
            {
                entityType = entityType.GetGenericArguments().First();
                list = Helpers.CreateGenericList(entityType, (entity as IEnumerable).Length());
                list.GetType().GetMethod("AddRange").Invoke(list, new object[] { entity });
            }

            if (this.CheckEntityAccessAllowed(entityType, this.GetKeyValues(entityType, list)))
            {
                foreach (var item in list)
                {
                    if (this.Entry(item).State != EntityState.Detached)
                    {
                        this.Set(entityType).Remove(item);
                    }
                }
            }
        }

        #endregion Delete

        #region SaveChanges

        /// <summary>
        /// Saves all changes to the data source.
        /// </summary>
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        #endregion SaveChanges

        #region Protected Methods

        /// <summary>
        /// Override this method to add additional security conditions to updating and deleting entities.
        /// </summary>
        /// <param name="entityType">The type of the entities</param>
        /// <param name="entityKeyValues">
        /// A list with an array of key values for all entities to check access for
        /// </param>
        /// <returns>True if access to the entities is allowed, false otherwise</returns>
        protected virtual bool CheckEntityAccessAllowed(Type entityType, IList<object[]> entityKeyValues)
        {
            return true;
        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        /// <param name="disposing">True if the context is being disposed, false otherwise</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the key property names for an entity type.
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <returns>The key property names</returns>
        protected string[] GetKeyProperties(Type entityType)
        {
            string[] keyProps;

            if (_keyPropertyNames.ContainsKey(entityType))
            {
                keyProps = _keyPropertyNames[entityType];
            }
            else
            {
                // Get the key values of the entity using the object context.
                var objectContext = ((IObjectContextAdapter)this).ObjectContext;
                var mdw = objectContext.MetadataWorkspace;
                var objectData = mdw.GetItems<EntityType>(DataSpace.OSpace).FirstOrDefault(x => x.FullName == entityType.FullName);
                var names = objectData.KeyMembers.Select(km => km.Name).ToArray();
                _keyPropertyNames.GetOrAdd(entityType, names);
                keyProps = _keyPropertyNames[entityType];
            }

            return keyProps;
        }

        /// <summary>
        /// Gets the key values for a list of entities.
        /// </summary>
        /// <param name="entityType">The type of the entities</param>
        /// <param name="entities">The entities</param>
        /// <returns>The list of the entities' key values</returns>
        protected List<object[]> GetKeyValues(Type entityType, IEnumerable entities)
        {
            string[] keyProps = this.GetKeyProperties(entityType);
            List<object[]> keyValues = new List<object[]>();

            foreach (var entity in entities)
            {
                var values = new List<object>(keyProps.Length);

                foreach (var prop in keyProps)
                {
                    values.Add(entity.GetPropertyValue(prop));
                }

                keyValues.Add(values.ToArray());
            }

            return keyValues;
        }

        /// <summary>
        /// Override this method to add additional security conditions to all queries.
        /// </summary>
        /// <param name="query">The query to secure</param>
        /// <returns>The secured query</returns>
        protected virtual IQueryable SecureQuery(IQueryable query)
        {
            return query;
        }

        #endregion Protected Methods

        #region Private Methods

        private static string GetConnection(string connectionStringName)
        {
            var connection = ModuleManager.ConnectionStrings[connectionStringName];

            if (connection == null)
            {
                return connectionStringName;
            }

            return connection.ConnectionString;
        }

        /// <summary>
        /// Gets the existing object with the key values of the passed object, if it exists.
        /// </summary>
        /// <param name="entity">The object with the key values to get the existing object for</param>
        /// <returns>The existing object, or NULL</returns>
        private object GetExistingObject(object entity)
        {
            var type = ObjectContext.GetObjectType(entity.GetType());
            var keyValues = this.GetKeyValues(type, new object[] { entity }).First();

            // If there are any Guid keys that have not been set, there is no existing entity yet
            // and no get needs to be attempted.
            if (keyValues.Any(v => v.GetType() == typeof(Guid) && v.Equals(Guid.Empty)))
            {
                return null;
            }

            // If there is only one key property and it is an integer, return null to prevent
            // returning an arbitrary object with an initial key value.
            if (keyValues.Length == 1)
            {
                var keyType = keyValues.First().GetType();
                var converter = TypeDescriptor.GetConverter(keyType);

                if (converter.CanConvertTo(typeof(long)) && converter.ConvertTo(keyValues.First(), typeof(long)).Equals((long)0))
                {
                    return null;
                }
            }

            // Try to find an existing object with the entity key values.
            return this.Set(type).Find(keyValues.ToArray());
        }

        /// <summary>
        /// Sets the Guid key for an entity that has a guid key and has an initial key value/
        /// </summary>
        /// <param name="entityType">The type of the entity</param>
        /// <param name="entity">The entity to set the key for</param>
        private void SetGuidKey(Type entityType, object entity)
        {
            var keyProps = this.GetKeyProperties(entityType);

            if (keyProps.Length == 1)
            {
                var name = keyProps.First();
                var prop = entityType.GetProperty(name);

                if (prop.PropertyType == typeof(Guid) && entity.GetPropertyValue(name).Equals(Guid.Empty))
                {
                    entity.SetPropertyValue(name, Guid.NewGuid());
                }
            }
        }

        #endregion Private Methods
    }
}