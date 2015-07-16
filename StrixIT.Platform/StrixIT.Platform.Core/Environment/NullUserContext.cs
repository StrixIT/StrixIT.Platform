//-----------------------------------------------------------------------
// <copyright file="NullUserContext.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    public class NullUserContext : IUserContext
    {
        public string Name
        {
            get 
            { 
                return null; 
            }
        }

        public string CurrentCulture
        {
            get
            {
                return null;
            }
        }

        public string CurrentUserName
        {
            get
            {
                return null;
            }
        }

        public Guid Id
        {
            get
            {
                return Guid.Empty;
            }
        }

        public Guid GroupId
        {
            get
            {
                return Guid.Empty;
            }
            set
            {
                return;
            }
        }

        public string GroupName
        {
            get
            {
                return null;
            }
            set
            {
                return;
            }
        }

        public IDictionary<Guid, string> Groups
        {
            get
            {
                return null;
            }
            set
            {
                return;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return true;
            }
        }

        public bool IsInMainGroup
        {
            get
            {
                return true;
            }
        }

        public bool HasPermission(string permission)
        {
            return true;
        }

        public bool HasPermission(IEnumerable<string> permissions)
        {
            return true;
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public bool IsInRoles(IEnumerable<string> roleNames)
        {
            return true;
        }
    }
}
