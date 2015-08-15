#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DataMapper.cs" company="StrixIT">
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

using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Extensions to easily map data between objects using Automapper.
    /// </summary>
    public static class DataMapper
    {
        #region Private Fields

        /// <summary>
        /// Cached AutoMapper methods retrieved with reflection.
        /// </summary>
        private static ConcurrentDictionary<string, MethodInfo> _autoMapperMethods = new ConcurrentDictionary<string, MethodInfo>();

        /// <summary>
        /// An action to ignore a member when mapping.
        /// </summary>
        private static Action<object, Type, LambdaExpression> _ignoreMember = (map, sourceType, property) =>
        {
            // Create the Ignore action type.
            var memberConfigExpressionType = typeof(IMemberConfigurationExpression<>).MakeGenericType(sourceType);
            var ignoreActionType = typeof(Action<>).MakeGenericType(memberConfigExpressionType);

            // Create the Ignore expression.
            var parameter = Expression.Parameter(memberConfigExpressionType, "c");
            var ignoreInfo = memberConfigExpressionType.GetMethod("Ignore", new Type[0]);
            var body = Expression.Call(parameter, ignoreInfo);
            var lambda = Expression.Lambda(body, parameter);

            // Invoke the ForMember method.
            var forMethod = map.GetType().GetMethod("ForMember", new Type[] { typeof(string), ignoreActionType });
            map = forMethod.Invoke(map, new object[] { GetPropertyName(property), lambda.Compile() });
        };

        /// <summary>
        /// An action to map a member when mapping.
        /// </summary>
        private static Action<object, Type, LambdaExpression, LambdaExpression> _mapMember = (map, sourceType, destinationPropertySelector, sourcePropertySelector) =>
        {
            // Get the property info.
            var isMemberExpression = sourcePropertySelector.Body as MemberExpression != null;
            var propertyType = isMemberExpression ? sourcePropertySelector.Body.Type : ((UnaryExpression)sourcePropertySelector.Body).Operand.Type;
            var name = GetPropertyName(sourcePropertySelector);

            // Create the MapFrom action type.
            var memberConfigExpressionType = typeof(IMemberConfigurationExpression<>).MakeGenericType(sourceType);
            var mapFromActionType = typeof(Action<>).MakeGenericType(memberConfigExpressionType);

            // Create the MapFrom expression.
            var mapParameter = Expression.Parameter(memberConfigExpressionType, "c");

            // I can use MapFrom here because there is only one such method in the current version
            // of AutoMapper. If that changes, find out how to get one with generic parameters.
            var mapFromInfo = memberConfigExpressionType.GetMethod("MapFrom").MakeGenericMethod(propertyType);
            var mapFromBody = Expression.Call(mapParameter, mapFromInfo, GetPropertySelector(sourceType, name));
            var mapFromLambda = Expression.Lambda(mapFromBody, mapParameter);

            // Invoke the ForMember method.
            var forMethod = map.GetType().GetMethod("ForMember", new Type[] { typeof(string), mapFromActionType });
            map = forMethod.Invoke(map, new object[] { GetPropertyName(destinationPropertySelector), mapFromLambda.Compile() });
        };

        /// <summary>
        /// The mapping configurations registered.
        /// </summary>
        private static ConcurrentBag<Tuple<Type, Type, List<object>>> _mappingConfigurations = new ConcurrentBag<Tuple<Type, Type, List<object>>>();

        #endregion Private Fields

        #region Public Events

        /// <summary>
        /// The event triggered when a map is created.
        /// </summary>
        public static event EventHandler<CreateMapEventArgs> OnCreateMap;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Creates a new map with Automapper.
        /// </summary>
        /// <typeparam name="TSource">The map source type</typeparam>
        /// <typeparam name="TDestination">The map destination type</typeparam>
        /// <returns>
        /// The mapping configuration created by Automapper, or NULL if the map already exists
        /// </returns>
        public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            return CreateMapping(typeof(TSource), typeof(TDestination)) as IMappingExpression<TSource, TDestination>;
        }

        /// <summary>
        /// Creates a new map with Automapper.
        /// </summary>
        /// <param name="sourceType">The map source type</param>
        /// <param name="destinationType">The map destination type</param>
        public static void CreateMap(Type sourceType, Type destinationType)
        {
            CreateMapping(sourceType, destinationType);
        }

        /// <summary>
        /// Maps an object to a different kind of object using AutoMapper.
        /// </summary>
        /// <typeparam name="T">The type of the object to map to</typeparam>
        /// <param name="entity">The object to map</param>
        /// <param name="target">The object to map to</param>
        /// <returns>The mapped object</returns>
        public static T Map<T>(this object entity, object target = null)
        {
            return (T)Map(entity, typeof(T), target);
        }

        /// <summary>
        /// Maps an object to a different kind of object using AutoMapper.
        /// </summary>
        /// <param name="source">The object to map</param>
        /// <param name="destinationType">The type of the object to map to</param>
        /// <param name="destination">The object to map to</param>
        /// <returns>The mapped object</returns>
        public static object Map(this object source, Type destinationType, object destination = null)
        {
            if (source == null)
            {
                return null;
            }

            if (destinationType == null)
            {
                throw new ArgumentNullException("targetType");
            }

            Type sourceType = source.GetType();

            if (sourceType.Namespace == PlatformConstants.ENTITYFRAMEWORKPROXYTYPE)
            {
                sourceType = sourceType.BaseType;
            }

            if (destinationType.Namespace == PlatformConstants.ENTITYFRAMEWORKPROXYTYPE)
            {
                destinationType = destinationType.BaseType;
            }

            CreateMap(sourceType, destinationType);

            if (destination != null)
            {
                Mapper.Map(source, destination, sourceType, destinationType);
            }
            else
            {
                destination = Mapper.Map(source, sourceType, destinationType);
            }

            ProcessMappingActions(sourceType, destinationType, source, destination);
            return destination;
        }

        /// <summary>
        /// Maps a collection of objects to a different kind of objects using AutoMapper.
        /// </summary>
        /// <typeparam name="T">The type of the objects to map to</typeparam>
        /// <param name="enumerable">The collection of objects to map</param>
        /// <returns>An enumerable containing the collection of mapped objects</returns>
        public static IEnumerable<T> Map<T>(this IEnumerable enumerable)
        {
            return enumerable.Map(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Maps a list of objects to the target type.
        /// </summary>
        /// <param name="enumerable">The list to map</param>
        /// <param name="destinationType">The target type</param>
        /// <returns>A list of target type objects with the values mapped</returns>
        public static IEnumerable Map(this IEnumerable enumerable, Type destinationType)
        {
            if (enumerable == null)
            {
                return Helpers.CreateGenericList(destinationType);
            }

            var query = Project(enumerable.AsQueryable(), destinationType);
            var list = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new Type[] { destinationType }).Invoke(null, new object[] { query }) as IEnumerable;
            ProcessMappingActions(query.ElementType, destinationType, list, null);
            return list;
        }

        /// <summary>
        /// Registers a new mapping configuration to use for mapping data using automapper.
        /// </summary>
        /// <typeparam name="TSource">The source object type</typeparam>
        /// <typeparam name="TDestination">The destination object type</typeparam>
        /// <param name="config">The mapping configuration</param>
        public static void RegisterMapConfig<TSource, TDestination>(MapConfig<TSource, TDestination> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var configurations = _mappingConfigurations.FirstOrDefault(c => c.Item1 == sourceType && c.Item2 == destinationType);

            if (configurations == null)
            {
                configurations = new Tuple<Type, Type, List<object>>(sourceType, destinationType, new List<object>());
                _mappingConfigurations.Add(configurations);
            }

            configurations.Item3.Add(config);
        }

        #endregion Public Methods

        #region Private Methods

        private static object CreateMapping(Type sourceType, Type destinationType)
        {
            if (Mapper.FindTypeMapFor(sourceType, destinationType) == null)
            {
                var createMapInfo = GetAutomapperMethod("CreateMap", 0, sourceType, destinationType);
                var map = createMapInfo.Invoke(null, null);

                if (OnCreateMap != null)
                {
                    var createMapEvent = new CreateMapEventArgs(sourceType, destinationType);
                    OnCreateMap(null, createMapEvent);

                    foreach (var propertyToIgnore in createMapEvent.PropertiesToIgnore)
                    {
                        _ignoreMember(map, sourceType, GetPropertySelector(destinationType, propertyToIgnore));
                    }

                    foreach (var propertyMap in createMapEvent.PropertiesToMap)
                    {
                        _mapMember(map, sourceType, GetPropertySelector(destinationType, propertyMap.Key), GetPropertySelector(sourceType, propertyMap.Value));
                    }
                }

                foreach (var config in GetMapConfigs(sourceType, destinationType))
                {
                    foreach (var member in config.GetPropertyValue("MembersToIgnore") as IEnumerable)
                    {
                        _ignoreMember(map, sourceType, (LambdaExpression)member);
                    }

                    foreach (var member in config.GetPropertyValue("MembersToMap") as IEnumerable)
                    {
                        _mapMember(map, sourceType, (LambdaExpression)member.GetPropertyValue("Key"), (LambdaExpression)member.GetPropertyValue("Value"));
                    }
                }

                return map;
            }

            return null;
        }

        private static MethodInfo GetAutomapperMethod(string name, int numberOfParameters, params Type[] parameters)
        {
            if (!_autoMapperMethods.ContainsKey(name))
            {
                var typeQuery = Assembly.GetAssembly(typeof(Mapper)).GetTypes().SelectMany(t => t.GetMethods());
                var info = typeQuery.FirstOrDefault(m => m.Name == name && m.GetGenericArguments().Count() == parameters.Count() && m.GetParameters().Count() == numberOfParameters);
                _autoMapperMethods.GetOrAdd(name, info);
            }

            return _autoMapperMethods[name].MakeGenericMethod(parameters);
        }

        private static IEnumerable GetMapConfigs(Type sourceType, Type destinationType)
        {
            return _mappingConfigurations.Where(c => c.Item1.IsAssignableFrom(sourceType) && c.Item2.IsAssignableFrom(destinationType)).SelectMany(c => c.Item3);
        }

        private static string GetPropertyName(LambdaExpression propertySelector)
        {
            var isMemberExpression = propertySelector.Body as MemberExpression != null;
            string name = null;

            if (isMemberExpression)
            {
                var selector = propertySelector.Body.ToString();
                name = selector.Substring(selector.IndexOf(".") + 1);
            }
            else
            {
                name = ((MemberExpression)((UnaryExpression)propertySelector.Body).Operand).Member.Name;
            }

            return name;
        }

        private static LambdaExpression GetPropertySelector(Type type, string property)
        {
            var sourceParameter = Expression.Parameter(type, "s");
            MemberExpression propertyChain = null;

            foreach (var part in property.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries))
            {
                propertyChain = Expression.Property(propertyChain == null ? (Expression)sourceParameter : (Expression)propertyChain, part);
            }

            return Expression.Lambda(propertyChain, sourceParameter);
        }

        private static void ProcessMappingActions(Type sourceType, Type destinationType, object source, object destination)
        {
            foreach (var config in GetMapConfigs(sourceType, destinationType))
            {
                var action = (dynamic)config.GetPropertyValue("AfterMapAction");

                if (action != null)
                {
                    var enumerable = source as IEnumerable;

                    if (enumerable != null)
                    {
                        foreach (var item in enumerable)
                        {
                            action(item, item);
                        }
                    }
                    else
                    {
                        action(source, destination);
                    }
                }
            }
        }

        private static IQueryable Project(this IQueryable query, Type typeToProjectTo)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (typeToProjectTo == null)
            {
                throw new ArgumentNullException("typeToProjectTo");
            }

            CreateMap(query.ElementType, typeToProjectTo);
            var projectMethodInvoke = GetAutomapperMethod("Project", 1, query.ElementType).Invoke(null, new object[] { query }) as IProjectionExpression;
            var toMethodInfo = projectMethodInvoke.GetType().GetMethod("To", new Type[] { typeof(object) }).MakeGenericMethod(typeToProjectTo);
            var toMethodInvoke = toMethodInfo.Invoke(projectMethodInvoke, new object[] { null }) as IQueryable;
            return toMethodInvoke;
        }

        #endregion Private Methods
    }
}