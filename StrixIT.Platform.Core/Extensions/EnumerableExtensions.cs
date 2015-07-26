#region Apache License
//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="StrixIT">
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
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Platform extensions for enumerables.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Trims all items in an enumerable of strings.
        /// </summary>
        /// <param name="enumerable">The enumerable of strings</param>
        /// <returns>The list of trimmed strings</returns>
        public static IList<string> Trim(this IEnumerable<string> enumerable)
        {
            var list = new List<string>(enumerable.Count());

            if (enumerable == null)
            {
                return list;
            }

            foreach (var item in enumerable)
            {
                list.Add(item.Trim());
            }

            return list;
        }

        /// <summary>
        /// An extension to allow Length to be called on non-generic enumerables.
        /// </summary>
        /// <param name="enumerable">The enumerable</param>
        /// <returns>The number of elements in the enumerable</returns>
        public static int Length(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            IEnumerator enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return 0;
            }

            int numberOfItems = 0;

            do
            {
                numberOfItems += 1;
            }
            while (enumerator.MoveNext());

            return numberOfItems;
        }

        /// <summary>
        /// Gets the first item from a non-generic enumerable, executing the query if the enumerable represents a query.
        /// </summary>
        /// <param name="enumerable">The enumerable</param>
        /// <returns>The first element in the enumerable, or NULL if the enumerable has no elements</returns>
        public static object GetFirst(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            // Use take to get the first item in the enumerable.
            enumerable = enumerable.AsQueryable().Take(1);
            object entity = null;

            // Use foreach to execute the query, when the enumerable represents one.
            foreach (var item in enumerable)
            {
                entity = item;
            }

            return entity;
        }

        /// <summary>
        /// Creates a list from a non-generic enumerable, executing the query if the enumerable represents one.
        /// </summary>
        /// <param name="enumerable">The enumerable to get the list from</param>
        /// <returns>The list</returns>
        public static IList GetList(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            var listType = enumerable.AsQueryable().ElementType;

            if (typeof(IList<>).IsAssignableFrom(listType))
            {
                return enumerable as IList;
            }

            var list = Helpers.CreateGenericList(listType, enumerable.Length());
            var addMethod = list.GetType().GetMethod("Add");

            foreach (var entry in enumerable)
            {
                addMethod.Invoke(list, new object[] { entry });
            }

            return list;
        }

        /// <summary>
        /// Convert all strings in a enumerable to lower case.
        /// </summary>
        /// <param name="enumerable">The enumerable to convert</param>
        /// <returns>The enumerable with all elements converted to lower case</returns>
        public static IList<string> ToLower(this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            List<string> list = new List<string>(enumerable.Count());

            foreach (string item in enumerable)
            {
                list.Add(item.ToLower(CultureInfo.CurrentCulture));
            }

            return list;
        }

        /// <summary>
        /// Checks whether an enumerable is null or contains no elements.
        /// </summary>
        /// <param name="enumerable">The enumerable to check</param>
        /// <returns>True if the enumerable is null or empty, false otherwise</returns>
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            return enumerable == null || enumerable.GetEnumerator().MoveNext() == false;
        }
    }
}