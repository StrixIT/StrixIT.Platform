//-----------------------------------------------------------------------
// <copyright file="GroupData.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to look up group names in the Cms when querying, as these are stored in another database
    /// which makes it impossible to join them in a query.
    /// </summary>
    public class GroupData : ValidationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupData" /> class.
        /// </summary>
        public GroupData() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupData" /> class.
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="groupName">The group name</param>
        public GroupData(Guid groupId, string groupName)
        {
            this.Id = groupId;
            this.Name = groupName;
        }

        /// <summary>
        /// Gets or sets the id of the group.
        /// </summary>
        [StrixRequiredWithMembershipAttribute]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        [StrixRequiredWithMembershipAttribute]
        [StringLength(250)]
        public string Name { get; set; }
    }
}