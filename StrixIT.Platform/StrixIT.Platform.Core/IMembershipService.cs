//-----------------------------------------------------------------------
// <copyright file="IMembershipService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface for the membership service.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Gets the id of the application.
        /// </summary>
        Guid ApplicationId { get; }

        /// <summary>
        /// Gets the id of the main group.
        /// </summary>
        Guid MainGroupId { get; }

        /// <summary>
        /// Gets the id of the admin user.
        /// </summary>
        Guid AdminId { get; }

        /// <summary>
        /// Initializes the membership service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets a query of user data.
        /// </summary>
        /// <returns>The user data</returns>
        IQueryable<UserData> UserData();

        /// <summary>
        /// Gets a query of group data.
        /// </summary>
        /// <returns>The group data</returns>
        IQueryable<GroupData> GroupData();
    }
}
