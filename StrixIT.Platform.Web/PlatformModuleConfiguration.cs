﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PlatformModuleConfiguration.cs" company="StrixIT">
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
using System.Collections.Generic;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The class for the basic configuration of the platform.
    /// </summary>
    public class PlatformModuleConfiguration : IModuleConfiguration
    {
        #region Public Properties

        public IList<ModuleLink> ModuleLinks
        {
            get
            {
                return new List<ModuleLink>();
            }
        }

        public IDictionary<string, IList<string>> ModulePermissions
        {
            get
            {
                var dictionary = new Dictionary<string, IList<string>>();

                var adminPermissions = new List<string>
                {
                    PlatformPermissions.AccessSite,
                    PlatformPermissions.ViewAdminDashboard
                };

                var userPermissions = new List<string>
                {
                    PlatformPermissions.AccessSite
                };

                dictionary.Add(PlatformConstants.ADMINROLE, adminPermissions);
                dictionary.Add(PlatformConstants.USERROLE, userPermissions);
                return dictionary;
            }
        }

        public string Name
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion Public Properties
    }
}