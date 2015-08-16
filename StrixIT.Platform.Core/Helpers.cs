#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="StrixIT">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    public static class Helpers
    {
        #region Public Methods

        /// <summary>
        /// Creates a generic list based on a type and an optional length.
        /// </summary>
        /// <param name="type">The type to create the list for</param>
        /// <param name="length">The initial length of the list</param>
        /// <returns>The generic list</returns>
        public static IList CreateGenericList(Type type, int length = 0)
        {
            var listType = typeof(List<>).MakeGenericType(type);
            var constructor = listType.GetConstructor(new Type[] { typeof(int) });
            return constructor.Invoke(new object[] { length }) as IList;
        }

        public static Expression<Func<T>> FuncToExpression<T>(Func<T> f)
        {
            return () => f();
        }

        /// <summary>
        /// Gets the value of a string as a .NET object of the proper type.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="type">The type</param>
        /// <returns>The value as the proper object type</returns>
        public static object GetTypedValue(string value, Type type)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var converter = new TypeConverter();
            object typedValue = null;
            bool isNullable = Nullable.GetUnderlyingType(type) != null;

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (typeof(Enum).IsAssignableFrom(type))
                {
                    var allValues = Enum.GetNames(type);
                    var match = allValues.First(v => v.ToLower().Contains(value.ToLower()));
                    typedValue = Enum.Parse(type, match);
                }
                else
                {
                    if (isNullable)
                    {
                        typedValue = Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
                    }
                    else
                    {
                        typedValue = Convert.ChangeType(value, type);
                    }
                }
            }
            else if (!isNullable)
            {
                typedValue = Activator.CreateInstance(type);
            }

            return typedValue;
        }

        public static string GetWorkingDirectory()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToLower();

            if (path.Contains("\\bin\\"))
            {
                path = path.Substring(0, path.IndexOf("\\bin\\"));
            }

            return path;
        }

        /// <summary>
        /// Checks whether a value has the default value for the type.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>True if the value is the default value for the type, false otherwise</returns>
        public static bool IsDefaultValue(object value)
        {
            Type objectType = value.GetType();

            if (objectType.IsValueType)
            {
                object defaultValue = Activator.CreateInstance(objectType);

                if (defaultValue.Equals(value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return value == null;
        }

        #endregion Public Methods
    }
}