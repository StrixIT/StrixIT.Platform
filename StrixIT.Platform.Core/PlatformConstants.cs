#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PlatformConstants.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Constants used in the platform.
    /// </summary>
    public static class PlatformConstants
    {
        #region Public Fields

        public const string ADMINROLE = "Administrator";
        public const string CONTRIBUTORROLE = "Contributor";
        public const string EDITORROLE = "Editor";
        public const string GROUPADMINROLE = "GroupAdministrator";
        public const string USERROLE = "RegisteredUser";
        public static readonly string CURRENTCULTURE = "CurrentCulture";
        public static readonly string CURRENTGROUPID = "CurrentGroupId";
        public static readonly string CURRENTUSER = "CurrentUser";
        public static readonly string CURRENTUSEREMAIL = "CurrentUserEmail";
        public static readonly string CURRENTUSERGROUPS = "CurrentUserGroups";
        public static readonly string ENTITYFRAMEWORKPROXYTYPE = "System.Data.Entity.DynamicProxies";
        public static readonly string LANGUAGE = "language";
        public static readonly string PLATFORM = "Platform";
        public static readonly string STRUCTUREMAPPRIVATE = "Private";

        #endregion Public Fields
    }
}