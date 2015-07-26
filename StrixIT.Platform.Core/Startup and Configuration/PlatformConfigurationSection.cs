#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PlatformConfigurationSection.cs" company="StrixIT">
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

using System.Configuration;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The class for configuring the StrixIT platform.
    /// </summary>
    public class PlatformConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the application name.
        /// </summary>
        [ConfigurationProperty("applicationName", IsRequired = true)]
        public string ApplicationName
        {
            get
            {
                return this["applicationName"] as string;
            }
        }

        /// <summary>
        /// Gets the cultures supported in the application.
        /// </summary>
        [ConfigurationProperty("cultures", IsRequired = false, DefaultValue = "en")]
        public string Cultures
        {
            get
            {
                return this["cultures"] as string;
            }
        }

        /// <summary>
        /// Gets the dependency white list prefix.
        /// </summary>
        [ConfigurationProperty("dependencyWhitelist", IsRequired = false, DefaultValue = "StrixIT")]
        public string DependencyWhitelist
        {
            get
            {
                return this["dependencyWhitelist"] as string;
            }
        }

        /// <summary>
        /// Gets the dependency injector type.
        /// </summary>
        [ConfigurationProperty("dependencyInjector", IsRequired = false, DefaultValue = "StrixIT.Platform.Core, StructureMapDependencyInjector")]
        public string DependencyInjector
        {
            get
            {
                return this["dependencyInjector"] as string;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the home controller of the application should be accessible only by authenticated users.
        /// </summary>
        [ConfigurationProperty("secureHomeController", IsRequired = false, DefaultValue = false)]
        public bool SecureHomeController
        {
            get
            {
                return (bool)this["secureHomeController"];
            }
        }
    }
}