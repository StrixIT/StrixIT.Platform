//-----------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Resources;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to allow retrieving resources from a resource type by name.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Gets a resource string from a resource.
        /// </summary>
        /// <param name="resourceType">The resource type</param>
        /// <param name="resourceName">The resource name</param>
        /// <returns>The resource string</returns>
        public static string GetResource(Type resourceType, string resourceName)
        {
            if (resourceType == null)
            {
                throw new ArgumentNullException("resourceType");
            }

            ResourceManager manager = new ResourceManager(resourceType.FullName, resourceType.Assembly);
            return manager.GetString(resourceName);
        }
    }
}
