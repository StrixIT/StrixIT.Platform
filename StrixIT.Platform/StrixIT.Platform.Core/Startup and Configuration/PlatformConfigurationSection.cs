//-----------------------------------------------------------------------
// <copyright file="PlatformConfigurationSection.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
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