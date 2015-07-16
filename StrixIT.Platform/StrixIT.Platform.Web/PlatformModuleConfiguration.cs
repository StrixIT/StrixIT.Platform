//-----------------------------------------------------------------------
// <copyright file="PlatformModuleConfiguration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The class for the basic configuration of the platform.
    /// </summary>
    public class PlatformModuleConfiguration : IModuleConfiguration
    {
        public string Name
        {
            get
            {
                return string.Empty;
            }
        }

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
    }
}