//-----------------------------------------------------------------------
// <copyright file="IWebModuleConfiguration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The interface to configure modules that need to register additional web components.
    /// </summary>
    public interface IWebModuleConfiguration : IModuleConfiguration
    {
        /// <summary>
        /// Gets the style bundles this module uses.
        /// </summary>
        IList<string> StyleBundles { get; }

        /// <summary>
        /// Gets the script bundles this module uses.
        /// </summary>
        IList<string> ScriptBundles { get; }
    }
}
