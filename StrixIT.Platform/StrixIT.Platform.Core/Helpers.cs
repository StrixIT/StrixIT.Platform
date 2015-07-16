//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;

namespace StrixIT.Platform.Core
{
    public static class Helpers
    {
        public static T GetConfigSectionGroup<T>(string group) where T : class
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = Path.Combine(StrixPlatform.Environment.WorkingDirectory, "web.config");
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            T settings = config.GetSectionGroup(group) as T;
            return settings;
        }

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

        /// <summary>
        /// Gets the injected implementation or the default implementation for a dependency.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the injected implementation</typeparam>
        /// <typeparam name="TDefault">The type of the default implementation</typeparam>
        /// <returns>The injected implementation when available, otherwise the default implementation</returns>
        public static TImplementation GetImplementationOrDefault<TImplementation, TDefault>()
            where TImplementation : class
            where TDefault : class
        {
            var type = GetInjectedOrDefaultType<TImplementation, TDefault>(true);
            var result = type != null ? Activator.CreateInstance(type) : Activator.CreateInstance<TDefault>();
            return result as TImplementation;
        }

        /// <summary>
        /// Gets the injected implementation or the default implementation for a dependency.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the injected implementation</typeparam>
        /// <typeparam name="TDefault">The type of the default implementation</typeparam>
        /// <param name="parameterlessContructor">True if the type has a parameterless constructor, false otherwise</param>
        /// <returns>The injected implementation when available, otherwise the default implementation</returns>
        public static Type GetInjectedOrDefaultType<TImplementation, TDefault>(bool parameterlessContructor = false)
            where TImplementation : class
            where TDefault : class
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetTypes().Any(t => typeof(TImplementation).IsAssignableFrom(t)))
                                                          .SelectMany(a => a.GetTypes())
                                                          .FirstOrDefault(t => !t.IsInterface
                                                                               && typeof(TImplementation).IsAssignableFrom(t)
                                                                               && !typeof(TDefault).IsAssignableFrom(t)
                                                                               && (!parameterlessContructor || t.GetConstructor(Type.EmptyTypes) != null));

            return type != null ? type : typeof(TDefault);
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
    }
}