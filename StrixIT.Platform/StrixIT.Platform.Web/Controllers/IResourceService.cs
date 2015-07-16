//-----------------------------------------------------------------------
// <copyright file="IResourceService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An interface for retrieving string resources and localized enums.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Gets the names and localized names for the enumerations that are part of the specified module.
        /// </summary>
        /// <param name="moduleName">The name of the module to get the enums for</param>
        /// <returns>A collection of names and localized names for the module enumerations</returns>
        ClientResourceCollection GetEnums(string moduleName);

        /// <summary>
        /// Gets the names and localized names resource strings that are part of the specified module.
        /// </summary>
        /// <param name="moduleName">The name of the module to get the resource strings for</param>
        /// <returns>A collection of names and localized names for the module resource strings</returns>
        ClientResourceCollection GetResx(string moduleName);
    }
}
