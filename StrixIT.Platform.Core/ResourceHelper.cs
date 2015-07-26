#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="StrixIT">
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
#endregion

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
