#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IResourceService.cs" company="StrixIT">
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