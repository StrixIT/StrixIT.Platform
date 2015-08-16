#region Apache License

//-----------------------------------------------------------------------
// <copyright file="NullUserContext.cs" company="StrixIT">
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

using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    public class NullUserContext : IUserContext
    {
        #region Public Properties

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

        public string Email
        {
            get
            {
                return null;
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

        public Guid Id
        {
            get
            {
                return Guid.Empty;
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

        public string Name
        {
            get
            {
                return null;
            }
        }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}