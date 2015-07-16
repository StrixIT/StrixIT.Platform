//-----------------------------------------------------------------------
// <copyright file="DataFilter.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Extension methods to filter IQueryables.
    /// </summary>
    public static class DataFilter
    {
        /// <summary>
        /// A dictionary containing the registered FilterSortMaps per type.
        /// </summary>
        private static ConcurrentDictionary<Type, List<FilterSortMap>> _mappings = new ConcurrentDictionary<Type, List<FilterSortMap>>();

        #region Filter Maps

        /// <summary>
        /// Registers a new FilterSortMap for the type specified.
        /// </summary>
        /// <typeparam name="T">The type to register the map for</typeparam>
        /// <param name="map">The map</param>
        public static void RegisterFilterMap<T>(FilterSortMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            var entityType = typeof(T);

            if (!_mappings.ContainsKey(entityType))
            {
                _mappings.GetOrAdd(entityType, new List<FilterSortMap>());
            }

            var mappings = _mappings.Where(ma => ma.Key.Equals(entityType)).SelectMany(ma => ma.Value).ToList();

            if (!mappings.Contains(map, new FilterSortMappingComparer()))
            {
                _mappings[entityType].Add(map);
            }
        }

        #endregion

        #region Filter, Sort and Page

        /// <summary>
        /// Filters a query
        /// </summary>
        /// <typeparam name="T">The type of the query</typeparam>
        /// <param name="query">The query to filter</param>
        /// <param name="options">The filter, sort and page options to use</param>
        /// <param name="skipPaging">True if paging should be skipped, false otherwise</param>
        /// <returns>The filtered, sorted and paged query</returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, FilterOptions options, bool skipPaging = false)
        {
            return FilterQuery(query as IQueryable, options, skipPaging).Cast<T>();
        }

        /// <summary>
        /// Filters a query
        /// </summary>
        /// <param name="query">The query to filter</param>
        /// <param name="options">The filter, sort and page options to use</param>
        /// <param name="skipPaging">True if paging should be skipped, false otherwise</param>
        /// <returns>The filtered, sorted and paged query</returns>
        public static IQueryable Filter(this IQueryable query, FilterOptions options, bool skipPaging = false)
        {
            return FilterQuery(query as IQueryable, options, skipPaging);
        }

        /// <summary>
        /// Sorts a query
        /// </summary>
        /// <typeparam name="T">The type of the query</typeparam>
        /// <param name="query">The query to sort</param>
        /// <param name="options">The sort and page options to use</param>
        /// <param name="skipPaging">True if paging should be skipped, false otherwise</param>
        /// <returns>The sorted and paged query</returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, FilterOptions options, bool skipPaging = false)
        {
            return SortQuery(query, options, skipPaging).Cast<T>();
        }

        /// <summary>
        /// Sorts a query
        /// </summary>
        /// <param name="query">The query to sort</param>
        /// <param name="options">The sort and page options to use</param>
        /// <param name="skipPaging">True if paging should be skipped, false otherwise</param>
        /// <returns>The sorted and paged query</returns>
        public static IQueryable Sort(this IQueryable query, FilterOptions options, bool skipPaging = false)
        {
            return SortQuery(query, options, skipPaging);
        }

        /// <summary>
        /// Pages a query.
        /// </summary>
        /// <typeparam name="T">The type of the query</typeparam>
        /// <param name="query">The query</param>
        /// <param name="options">The page options to use</param>
        /// <returns>The paged query</returns>
        public static IQueryable<T> Page<T>(this IQueryable<TargetException> query, FilterOptions options)
        {
            return PageQuery(query, options).Cast<T>();
        }

        /// <summary>
        /// Pages a query.
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="options">The page options to use</param>
        /// <returns>The paged query</returns>
        public static IQueryable Page(this IQueryable query, FilterOptions options)
        {     
            return PageQuery(query, options);
        }

        #endregion

        #region Private Methods

        private static FilterSortMap GetFilterMap(Type entityType, string fieldName)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType");
            }

            if (fieldName == null)
            {
                throw new ArgumentNullException("fieldName");
            }

            var mapping = _mappings.Where(ma => ma.Key.IsAssignableFrom(entityType)).SelectMany(ma => ma.Value).FirstOrDefault(ma => ma.FieldToMap.ToLower().Equals(fieldName.ToLower()));
            return mapping;
        }

        private static IQueryable FilterQuery(IQueryable query, FilterOptions options, bool skipPaging)
        {
            if (query == null)
            {
                return null;
            }

            CheckFilter(options);

            if (options != null)
            {
                var entityType = query.ElementType;
                string combinedFilterString = null;
                List<object> parameters = new List<object>(options.Filter.Filters.Count());
                int index = 0;

                foreach (FilterField filterField in options.Filter.Filters)
                {
                    // If the filterfield is empty, continue.
                    if (string.IsNullOrWhiteSpace(filterField.Field) || string.IsNullOrWhiteSpace(filterField.Value))
                    {
                        continue;
                    }

                    var fieldName = filterField.Field.ToLower();
                    string filterString = null;

                    // Try to find a filter map for this field.
                    FilterSortMap map = GetFilterMap(entityType, fieldName);

                    // Invoke the field map when available, otherwise use the default filter code.
                    if (map != null)
                    {
                        if (map.FilterMap != null)
                        {
                            filterString = map.FilterMap(filterField.Operator, filterField.Value);
                        }
                    }
                    else
                    {
                        var fieldType = entityType.GetProperties().Where(p => p.Name.ToLower() == fieldName).Select(p => p.PropertyType).FirstOrDefault();

                        if (fieldType == null)
                        {
                            continue;
                        }

                        var typedValue = Helpers.GetTypedValue(filterField.Value, fieldType);
                        var propertyName = filterField.Field.Substring(0, 1).ToTitleCase() + filterField.Field.Substring(1);
                        string nullableValue = Nullable.GetUnderlyingType(entityType.GetProperty(propertyName).PropertyType) != null ? ".Value" : string.Empty;
                        string toLower = string.Empty;

                        if (typedValue.GetType() == typeof(string))
                        {
                            typedValue = typedValue.ToString().ToLower();
                            toLower = ".ToLower()";
                        }

                        if (typeof(Enum).IsAssignableFrom(fieldType))
                        {
                            filterString = string.Format("{0} == (@{1})", filterField.Field, index);
                        }
                        else
                        {
                            filterString = string.Format("{0}{1}{2}.{3}(@{4})", filterField.Field, nullableValue, toLower, filterField.Operator.ToString(), index);
                        }

                        parameters.Add(typedValue);
                        index++;
                    }

                    // Combine the new string and parameter with the already available ones.
                    combinedFilterString = combinedFilterString == null ? filterString : combinedFilterString + " " + options.Filter.Logic.ToString() + " " + filterString;
                }

                if (!string.IsNullOrWhiteSpace(combinedFilterString))
                {
                    query = query.Where(combinedFilterString, parameters.ToArray());
                }
            }

            return query.Sort(options, skipPaging);
        }

        private static IQueryable SortQuery(IQueryable query, FilterOptions options, bool skipPaging)
        {
            var entityType = query.ElementType;

            // If no filter field is specified, find the default property and sort on that.
            if (options == null || options.Sort.IsEmpty())
            {
                var property = GetSortProperty(entityType);
                query = query.OrderBy(property);
            }
            else
            {
                foreach (var field in options.Sort)
                {
                    FilterSortMap map = GetFilterMap(entityType, field.Field);

                    // If there is a sort map for this field, invoke it. Otherwise, use the default.
                    if (map != null)
                    {
                        if (map.SortMap != null)
                        {
                            query = map.SortMap(query, field.Dir);
                            continue;
                        }
                    }

                    query = query.OrderBy(string.Format(field.Field + " {0}", field.Dir));
                }
            }

            if (!skipPaging)
            {
                query = Page(query, options);
            }
            else
            {
                options.Total = query.Count();
            }

            return query;
        }

        private static IQueryable PageQuery(this IQueryable query, FilterOptions options)
        {
            query.SetTraverseList(options);

            if (options != null && options.Page > 0 && options.PageSize > 0)
            {
                int startIndex = Math.Max(options.Page - 1, 0);
                query = query.Skip(startIndex * options.PageSize).Take(options.PageSize);
            }

            return query;
        }

        private static void SetTraverseList(this IQueryable query, FilterOptions filter)
        {
            if (filter != null)
            {
                int? length = null;

                if (filter.TraverseList == null && filter.IncludeTraverseList && query.ElementType.HasProperty(filter.TraverseListPropertyName))
                {
                    int index = 0;
                    var result = query.Select(filter.TraverseListPropertyName).GetList().Cast<object>().ToArray();
                    length = result.Length;
                    var list = new List<TraverseList>(length.Value);

                    foreach (var entry in result)
                    {
                        list.Add(new TraverseList
                        {
                            PreviousId = index > 0 ? result[index - 1] : null,
                            Id = entry,
                            NextId = index < length - 1 ? result[index + 1] : null
                        });

                        index++;
                    }

                    filter.TraverseList = list;
                }

                if (filter.Total == 0)
                {
                    if (!length.HasValue)
                    {
                        length = query.Count();
                    }

                    filter.Total = length.Value;
                }
            }
        }

        private static void CheckFilter(FilterOptions options)
        {
            if (options != null && options.Page > 0 && options.Page < 1)
            {
                throw new ArgumentException("The page index must be greater than 0.");
            }

            if (options != null && options.Page > 0 && options.PageSize < 0)
            {
                throw new ArgumentException("The page size must be equal to or greater than 0.");
            }

            if (options != null && options.Page > 0)
            {
                long upperBound = ((long)options.Page * options.PageSize) + options.PageSize - 1;

                if (upperBound > int.MaxValue)
                {
                    throw new ArgumentException("The combination of page index and page size is invalid.");
                }
            }
        }

        private static string GetSortProperty(Type type)
        {
            PropertyInfo prop = null;

            prop = type.GetProperties().Where(pr => pr.Name.ToLower().Equals("id")).FirstOrDefault();

            if (prop == null)
            {
                prop = type.GetProperties().Where(pr => pr.PropertyType.Equals(typeof(DateTime)) || pr.PropertyType.Equals(typeof(DateTime?))).FirstOrDefault();
            }

            if (prop == null)
            {
                prop = type.GetProperties().FirstOrDefault();
            }

            if (prop == null)
            {
                throw new ArgumentException("The specified type has no properties.");
            }

            return prop.Name;
        }

        #endregion
    }
}