#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ModuleManager.cs" company="StrixIT">
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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The module manager handles loading all module assemblies and configuration.
    /// </summary>
    public static class ModuleManager
    {
        private static object _loadAssembliesLock = new object();
        private static object _loadConfigLock = new object();
        private static bool _assembliesLoaded = false;
        private static bool _configurationsLoaded = false;
        private static IList<Assembly> _assemblies = null;
        private static Dictionary<string, IDictionary<string, string>> _combinedAppSettings;
        private static ConnectionStringSettingsCollection _combinedConnections;

        /// <summary>
        /// Gets all loaded assemblies.
        /// </summary>
        public static IList<Assembly> LoadedAssemblies
        {
            get
            {
                if (!_assembliesLoaded)
                {
                    LoadAssemblies();
                }

                return _assemblies;
            }
        }

        /// <summary>
        /// Gets the connection strings from all loaded config files.
        /// </summary>
        /// <returns>A dictionary of connection string names and connection strings</returns>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (!_configurationsLoaded)
                {
                    LoadConfigurations();
                }

                return _combinedConnections;
            }
        }

        /// <summary>
        /// Gets the app settings from all loaded config files.
        /// </summary>
        /// <returns>A dictionary containing the app settings per module</returns>
        public static IDictionary<string, IDictionary<string, string>> AppSettings
        {
            get
            {
                if (!_configurationsLoaded)
                {
                    LoadConfigurations();
                }

                return _combinedAppSettings;
            }
        }

        /// <summary>
        /// Gets all types that can be assigned from the specified type from all loaded assemblies.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <returns>The types</returns>
        public static IList<Type> GetTypeList(Type baseType)
        {
            return LoadedAssemblies.SelectMany(a => a.GetTypes().Where(t => baseType.IsAssignableFrom(t))).ToList();
        }

        /// <summary>
        /// Gets instances of all non-abstract types that can be assigned from the specified type from all loaded assemblies.
        /// </summary>
        /// <typeparam name="T">The base type</typeparam>
        /// <returns>The instances</returns>
        public static IList<T> GetObjectList<T>()
        {
            var list = new List<T>();
            var types = GetTypeList(typeof(T));

            foreach (var type in types)
            {
                if (!type.IsAbstract)
                {
                    list.Add((T)Activator.CreateInstance(type));
                }
            }

            return list;
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
        /// Loads all module assemblies.
        /// </summary>
        public static void LoadAssemblies()
        {
            if (!_assembliesLoaded)
            {
                lock (_loadAssembliesLock)
                {
                    if (!_assembliesLoaded)
                    {
                        StrixPlatform.WriteStartupMessage("Load module assemblies.");

                        List<string> dlls = new List<string>();
                        var binPath = Path.Combine(StrixPlatform.Environment.WorkingDirectory, "bin");
                        var modulePath = Path.Combine(StrixPlatform.Environment.WorkingDirectory, "Areas");

                        var dependencyWhiteList = StrixPlatform.Configuration.DependencyWhitelist.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Trim().ToLower();

                        _assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => dependencyWhiteList.Contains(a.FullName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).First().ToLower())).ToList();
                        var loadedAssemblyNames = _assemblies.Select(assembly => assembly.FullName.Split(',').First().ToLower()).ToList();

                        if (Directory.Exists(binPath))
                        {
                            dlls.AddRange(Directory.GetFiles(binPath, "*.dll", SearchOption.AllDirectories)
                                .Where(a => dependencyWhiteList.Contains(a.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Last().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).First().ToLower())).ToList());
                        }

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
                        StrixPlatform.WriteStartupMessage("Completed loading assemblies");
                        _assembliesLoaded = true;
                    }
                }
            }
        }

        public static void LoadConfigurations()
        {
            if (!_configurationsLoaded)
            {
                lock (_loadConfigLock)
                {
                    if (!_configurationsLoaded)
                    {
                        _combinedConnections = new ConnectionStringSettingsCollection();
                        _combinedAppSettings = new Dictionary<string, IDictionary<string, string>>();
                        _combinedAppSettings.Add("Platform", new Dictionary<string, string>());
                        var platformSettings = ConfigurationManager.AppSettings;

                        foreach (string key in platformSettings.Keys)
                        {
                            _combinedAppSettings["Platform"].Add(key, platformSettings[key].ToLower());
                        }

                        foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
                        {
                            _combinedConnections.Add(connection);
                        }

                        var moduleDirectory = Path.Combine(StrixPlatform.Environment.WorkingDirectory, "Areas");

                        if (Directory.Exists(moduleDirectory))
                        {
                            string[] configFilePaths = Directory.GetFiles(moduleDirectory, "web.config", SearchOption.AllDirectories);

                            foreach (string configFilePath in configFilePaths)
                            {
                                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                                configMap.ExeConfigFilename = configFilePath;
                                var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                                LoadConnectionStrings(config);
                                LoadAppSettings(config);
                            }
                        }

                        _configurationsLoaded = true;
                    }
                }
            }
        }

        private static Type GetObjectType(string typeName, Func<Type, bool> func)
        {
            var assembly = LoadedAssemblies.Where(a => a.GetTypes().Any(func)).FirstOrDefault();

            if (assembly == null)
            {
                return null;
            }

            return assembly.GetTypes().First(func);
        }

        private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly != null)
            {
                return args.RequestingAssembly;
            }

            return _assemblies.SingleOrDefault(x => x.FullName == args.Name);
        }

        private static void LoadConnectionStrings(Configuration config)
        {
            ConnectionStringsSection section = null;

            try
            {
                section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            }
            catch (ConfigurationErrorsException ex)
            {
                string duplicateName = Regex.Match(ex.BareMessage, "'([^']*')").ToString();
                var message = string.Format("While reading the module connection strings, multiple entries with the name {0} were found. Please make sure the connection strings of the platform and the modules all have unique names.", duplicateName);
                StrixPlatform.WriteStartupMessage(message, LogLevel.Error);
                throw new ConfigurationErrorsException(message);
            }

            if (section == null)
            {
                return;
            }

            foreach (ConnectionStringSettings cs in section.ConnectionStrings)
            {
                if (_combinedConnections[cs.Name.ToLower()] == null)
                {
                    _combinedConnections.Add(new ConnectionStringSettings(cs.Name.ToLower(), cs.ConnectionString));
                }
            }
        }

        private static void LoadAppSettings(Configuration config)
        {
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section == null)
            {
                return;
            }

            var pathParts = config.FilePath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            var moduleName = pathParts.ElementAt(pathParts.Length - 2);

            if (moduleName.ToLower() == "views")
            {
                return;
            }

            if (!_combinedAppSettings.ContainsKey(moduleName))
            {
                _combinedAppSettings.Add(moduleName, new Dictionary<string, string>());

                foreach (KeyValueConfigurationElement s in section.Settings)
                {
                    _combinedAppSettings[moduleName].Add(s.Key, s.Value.ToLower());
                }
            }
        }
    }
}