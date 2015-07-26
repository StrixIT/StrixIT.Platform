#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ResourceService.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace StrixIT.Platform.Web
{
    public class ResourceService : IResourceService
    {
        private const string RESOURCEIDENTIFIER = "client";

        public ClientResourceCollection GetEnums(string moduleName)
        {
            moduleName = moduleName.ToLower();
            var result = new ClientResourceCollection();
            var loadedEnumTypes = ModuleManager.LoadedAssemblies.SelectMany(a =>
            {
                var assemblyName = a.FullName.Split(',').First().Split('.').Last().ToLower();
                return a.GetTypes().Where(t => typeof(Enum).IsAssignableFrom(t) && t.HasAttribute(typeof(ClientEnumAttribute)) && moduleName == assemblyName);
            });

            var cultureInfo = new CultureInfo(StrixPlatform.CurrentCultureCode);

            foreach (var enumType in loadedEnumTypes)
            {
                var displayAttribute = enumType.GetAttribute<ClientEnumAttribute>();
                ResourceManager manager = null;

                if (displayAttribute != null && displayAttribute.ResourceType != null)
                {
                    manager = new ResourceManager(displayAttribute.ResourceType);
                }

                result.Add(enumType.Name, Enum.GetNames(enumType).ToDictionary(k => k, v => manager != null ? manager.GetString(v, cultureInfo) ?? v : v));
            }

            return result;
        }

        public ClientResourceCollection GetResx(string moduleName)
        {
            moduleName = moduleName.ToLower();
            var resourceTypes = ModuleManager.LoadedAssemblies.Where(a =>
            {
                var assemblyName = a.FullName.Split(',').First().Split('.').Last().ToLower();
                return moduleName == assemblyName;
            }).SelectMany(a => a.GetTypes()).Where(t => t.Name.ToLower().Contains(RESOURCEIDENTIFIER));

            var result = new ClientResourceCollection();

            foreach (var type in resourceTypes)
            {
                var manager = new ResourceManager(type.FullName, type.Assembly);
                ResourceSet resourceSet;

                try
                {
                    resourceSet = manager.GetResourceSet(new CultureInfo(StrixPlatform.CurrentCultureCode), true, true);
                }
                catch (System.Resources.MissingManifestResourceException)
                {
                    continue;
                }

                System.Collections.IDictionaryEnumerator dictionaryEnumerator = resourceSet.GetEnumerator();
                var typeName = type.Name.ToLower();
                var key = typeName.Substring(0, typeName.IndexOf(RESOURCEIDENTIFIER));
                result.Add(key, new Dictionary<string, string>());
                var entry = result[key];

                // Get all string resources
                while (dictionaryEnumerator.MoveNext())
                {
                    // Only string resources
                    if (dictionaryEnumerator.Value is string)
                    {
                        var resourceKey = (string)dictionaryEnumerator.Key;
                        resourceKey = resourceKey.ToCamelCase();
                        var value = (string)dictionaryEnumerator.Value;
                        entry.Add(resourceKey, value);
                    }
                }
            }

            return result;
        }
    }
}