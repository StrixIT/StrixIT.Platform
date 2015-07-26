#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IDataSource.cs" company="StrixIT">
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

using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The base interface for all data sources.
    /// </summary>
    public interface IDataSource
    {
        #region Public Methods

        /// <summary>
        /// Deletes one or more entities from the data source. To persist the delete, SaveChanges
        /// should be called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to delete</typeparam>
        /// <param name="entity">The entity or entities to delete</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Gets a query for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The query</returns>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Saves one or more entities to the data source. To persist them, SaveChanges should be
        /// called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to save</typeparam>
        /// <param name="entity">The entity or entities to save</param>
        /// <returns>The saved entity or entities</returns>
        T Save<T>(T entity) where T : class;

        /// <summary>
        /// Saves all changes to the data source.
        /// </summary>
        void SaveChanges();

        #endregion Public Methods
    }
}