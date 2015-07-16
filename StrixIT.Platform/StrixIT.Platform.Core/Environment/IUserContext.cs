//-----------------------------------------------------------------------
// <copyright file="IUserContext.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to abstract away the current user.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the current user's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the current user's id, or an empty GUID if no user is currently active.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the current user's current group id, or the id of the main group if no user is currently active.
        /// </summary>
        Guid GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the group the user is currently in.
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of group ids and names the user can access.
        /// </summary>
        IDictionary<Guid, string> Groups { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current user is an administrator.
        /// </summary>
        bool IsAdministrator { get; }

        /// <summary>
        /// Gets a value indicating whether the current user is a member of the main group.
        /// </summary>
        bool IsInMainGroup { get; }

        /// <summary>
        /// Checks whether the currently active user is in one of the specified roles.
        /// </summary>
        /// <param name="roleNames">The roles to check membership for</param>
        /// <returns>True if the user is in one of the roles, false otherwise</returns>
        bool IsInRoles(IEnumerable<string> roleNames);

        /// <summary>
        /// Checks whether the currently active user is in the specified role.
        /// </summary>
        /// <param name="roleName">The role to check membership for</param>
        /// <returns>True if the user is in the role, false otherwise</returns>
        bool IsInRole(string roleName);

        /// <summary>
        /// Checks whether the currently active user has one of the specified permissions.
        /// </summary>
        /// <param name="permissions">The permissions to check</param>
        /// <returns>True if the user has one of the specified permissions, false otherwise</returns>
        bool HasPermission(IEnumerable<string> permissions);

        /// <summary>
        /// Checks whether the currently active user has the specified permission.
        /// </summary>
        /// <param name="permission">The permission to check</param>
        /// <returns>True if the user has the specified permission, false otherwise</returns>
        bool HasPermission(string permission);
    }
}
