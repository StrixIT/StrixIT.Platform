#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DependencyInjector.cs" company="StrixIT">
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

using StrixIT.Platform.Core.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    public static class DependencyInjector
    {
        #region Private Fields

        private static IList<Assembly> _assemblies = null;
        private static bool _assembliesLoaded = false;
        private static object _loadAssembliesLock = new object();

        #endregion Private Fields

        #region Public Properties

        public static IDependencyInjector Injector { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static T Get<T>()
        {
            return Injector.Get<T>();
        }

        public static T Get<T>(string instanceKey)
        {
            return Injector.Get<T>(instanceKey);
        }

        public static object Get(Type type)
        {
            return Injector.Get(type);
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return Injector.GetAll<T>();
        }

        public static IEnumerable GetAll(Type dependencyType)
        {
            return Injector.GetAll(dependencyType);
        }

        /// <summary>
        /// Gets all loaded assemblies.
        /// </summary>
        public static IList<Assembly> GetLoadedAssemblies()
        {
            if (!_assembliesLoaded)
            {
                LoadAssemblies();
            }

            return _assemblies;
        }

        /// <summary>
        /// Gets an object type by its full name from any of the loaded assemblies.
        /// </summary>
        /// <param name="typeName">The full name of the type</param>
        /// <returns>The type</returns>
        public static Type GetObjectTypeByFullName(string typeName)
        {
            return GetObjectType(typeName, x => x.FullName.ToLower() == typeName.ToLower());
        }

        /// <summary>
        /// Gets an object type by its name from any of the loaded assemblies.
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <returns>The type</returns>
        public static Type GetObjectTypeByName(string typeName)
        {
            return GetObjectType(typeName, x => x.Name.ToLower() == typeName.ToLower());
        }

        /// <summary>
        /// Gets all types that can be assigned from the specified type from all loaded assemblies.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <returns>The types</returns>
        public static IList<Type> GetTypeList(Type baseType)
        {
            return GetLoadedAssemblies().SelectMany(a => a.GetTypes().Where(t => baseType.IsAssignableFrom(t))).ToList();
        }

        public static T TryGet<T>()
        {
            return Injector.TryGet<T>();
        }

        public static T TryGet<T>(string instanceKey)
        {
            return Injector.TryGet<T>(instanceKey);
        }

        public static object TryGet(Type dependencyType)
        {
            return Injector.TryGet(dependencyType);
        }

        #endregion Public Methods

        #region Private Methods

        private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly != null)
            {
                return args.RequestingAssembly;
            }

            return _assemblies.SingleOrDefault(x => x.FullName == args.Name);
        }

        private static Type GetObjectType(string typeName, Func<Type, bool> func)
        {
            var assembly = GetLoadedAssemblies().Where(a => a.GetTypes().Any(func)).FirstOrDefault();

            if (assembly == null)
            {
                return null;
            }

            return assembly.GetTypes().First(func);
        }

        private static void LoadAssemblies()
        {
            if (!_assembliesLoaded)
            {
                lock (_loadAssembliesLock)
                {
                    if (!_assembliesLoaded)
                    {
                        List<string> dlls = new List<string>();
                        var domain = AppDomain.CurrentDomain;
                        var modulePath = Path.Combine(domain.BaseDirectory, "Areas");
                        _assemblies = domain.GetAssemblies().Where(a => !a.IsDynamic && !a.FullName.Contains("mscorlib")).ToList();
                        var loadedAssemblyNames = _assemblies.Select(assembly => assembly.FullName.Split(',').First().ToLower()).ToList();

                        if (Directory.Exists(modulePath))
                        {
                            dlls.AddRange(Directory.GetFiles(modulePath, "*.dll", SearchOption.AllDirectories));
                        }

                        foreach (string dll in dlls)
                        {
                            try
                            {
                                string assemblyName = dll.Substring(dll.LastIndexOf('\\') + 1).Replace(".dll", string.Empty).ToLower();

                                if (loadedAssemblyNames.Contains(assemblyName))
                                {
                                    continue;
                                }

                                _assemblies.Add(Assembly.LoadFrom(dll));
                                loadedAssemblyNames.Add(assemblyName);
                            }
                            catch (FileLoadException)
                            {
                                StrixPlatform.WriteStartupMessage(string.Format("Assembly {0} has already been loaded.", dll), LogLevel.Error);
                            }
                            catch (BadImageFormatException)
                            {
                                StrixPlatform.WriteStartupMessage(string.Format("File {0} is not an assembly.", dll), LogLevel.Error);
                            }
                            catch (NotSupportedException)
                            {
                                StrixPlatform.WriteStartupMessage(string.Format("Error loading assembly {0}.", dll), LogLevel.Error);
                            }
                        }

                        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;
                        _assembliesLoaded = true;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}