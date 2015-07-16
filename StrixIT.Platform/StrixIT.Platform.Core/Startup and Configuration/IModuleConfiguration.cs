//-----------------------------------------------------------------------
// <copyright file="IModuleConfiguration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to configure modules.
    /// </summary>
    public interface IModuleConfiguration
    {
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the links to the content for this module on the admin dashboard.
        /// </summary>
        IList<ModuleLink> ModuleLinks { get; }

        /// <summary>
        /// Gets the roles and associated permissions that are defined by this module.
        /// </summary>
        IDictionary<string, IList<string>> ModulePermissions { get; }
    }
}
