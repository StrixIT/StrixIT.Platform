//-----------------------------------------------------------------------
// <copyright file="PlatformConstants.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Constants used in the platform.
    /// </summary>
    public static class PlatformConstants
    {
        public const string ADMINROLE = "Administrator";
        public const string GROUPADMINROLE = "GroupAdministrator";
        public const string USERROLE = "RegisteredUser";
        public const string EDITORROLE = "Editor";
        public const string CONTRIBUTORROLE = "Contributor";

        public static readonly string STRUCTUREMAPPRIVATE = "Private";
        public static readonly string CURRENTCULTURE = "CurrentCulture";
        public static readonly string LANGUAGE = "language";
        public static readonly string ENTITYFRAMEWORKPROXYTYPE = "System.Data.Entity.DynamicProxies";
        public static readonly string CURRENTUSEREMAIL = "CurrentUserEmail";
        public static readonly string CURRENTUSER = "CurrentUser";
        public static readonly string CURRENTGROUPID = "CurrentGroupId";
        public static readonly string CURRENTUSERGROUPS = "CurrentUserGroups";
        public static readonly string PLATFORM = "Platform";
    }
}