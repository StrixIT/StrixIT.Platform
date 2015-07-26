#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IModuleConfiguration.cs" company="StrixIT">
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

using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to configure modules.
    /// </summary>
    public interface IModuleConfiguration
    {
        #region Public Properties

        /// <summary>
        /// Gets the links to the content for this module on the admin dashboard.
        /// </summary>
        IList<ModuleLink> ModuleLinks { get; }

        /// <summary>
        /// Gets the roles and associated permissions that are defined by this module.
        /// </summary>
        IDictionary<string, IList<string>> ModulePermissions { get; }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        string Name { get; }

        #endregion Public Properties
    }
}