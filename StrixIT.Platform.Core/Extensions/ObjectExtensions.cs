#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="StrixIT">
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
using System.Globalization;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Platform extensions for objects.
    /// </summary>
    public static class ObjectExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the attribute of the specified type from the type, propertyinfo or object, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to get</typeparam>
        /// <param name="entity">The type, property info or object to get the attribute for</param>
        /// <returns>The attribute, if the type, property info or object has it</returns>
        public static T GetAttribute<T>(this object entity) where T : Attribute
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity is Type)
            {
                return ((Type)entity).GetCustomAttribute<T>();
            }
            else if (entity is PropertyInfo)
            {
                return ((PropertyInfo)entity).GetCustomAttribute<T>();
            }
            else
            {
                return entity.GetType().GetCustomAttribute<T>();
            }
        }

        /// <summary>
        /// Gets the value of the specified property of the object.
        /// </summary>
        /// <param name="entity">The object to get the property value of</param>
        /// <param name="propertyName">The name of the property to get the value for</param>
        /// <returns>The property value</returns>
        public static object GetPropertyValue(this object entity, string propertyName)
        {
            PropertyInfo info = GetProperty(entity, propertyName);
            return info.GetValue(entity);
        }

        /// <summary>
        /// Checks whether the type, propertyinfo or object has the specified attribute.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to check for</typeparam>
        /// <param name="entity">The type, property info or object to check the attribute for</param>
        /// <returns>True if the attribute is present, false otherwise</returns>
        public static bool HasAttribute<T>(this object entity) where T : Attribute
        {
            return GetAttribute<T>(entity) != null;
        }

        /// <summary>
        /// Gets a value indicating whether the specified type has an attribute of the specified
        /// attribute type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="attributeType">The type of the attribute to check for</param>
        /// <param name="includeProperties">
        /// True if the properties of the type should be included in the search, false otherwise
        /// </param>
        /// <returns>True if the type has the property, false otherwise</returns>
        public static bool HasAttribute(this Type type, Type attributeType, bool includeProperties = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var found = type.GetCustomAttribute(attributeType) != null;

            if (!found && includeProperties)
            {
                foreach (var property in type.GetProperties())
                {
                    found = property.GetCustomAttribute(attributeType) != null;

                    if (found)
                    {
                        break;
                    }
                }
            }

            return found;
        }

        /// <summary>
        /// Gets a value indicating whether the type or object has a property with the specified name.
        /// </summary>
        /// <param name="entity">The type or object</param>
        /// <param name="propertyName">The name of the property to check for</param>
        /// <returns>True if the type or object has the property, false otherwise</returns>
        public static bool HasProperty(this object entity, string propertyName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Type type;

            if (typeof(Type).IsAssignableFrom(entity.GetType()))
            {
                type = entity as Type;
            }
            else
            {
                type = entity.GetType();
            }

            PropertyInfo info = type.GetProperty(propertyName);
            return info != null;
        }

        /// <summary>
        /// Sets the value of the specified property of the object.
        /// </summary>
        /// <param name="entity">The object to set the property value of</param>
        /// <param name="propertyName">The name of the property to set the value for</param>
        /// <param name="propertyValue">The value for the property</param>
        public static void SetPropertyValue(this object entity, string propertyName, object propertyValue)
        {
            PropertyInfo info = GetProperty(entity, propertyName);
            info.SetValue(entity, propertyValue);
        }

        /// <summary>
        /// Converts a string to camel case.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The camel cased string</returns>
        public static string ToCamelCase(this string input)
        {
            return input.Substring(0, 1).ToLower() + input.Substring(1);
        }

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The title cased string</returns>
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return new CultureInfo(StrixPlatform.CurrentCultureCode).TextInfo.ToTitleCase(input);
        }

        #endregion Public Methods

        #region Private Methods

        private static PropertyInfo GetProperty(object entity, string propertyName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            Type type = entity.GetType();
            PropertyInfo info = type.GetProperty(propertyName);

            if (info == null)
            {
                throw new ArgumentException(string.Format("Type {0} does not have a property with name {1}", type.Name, propertyName));
            }

            return info;
        }

        #endregion Private Methods
    }
}