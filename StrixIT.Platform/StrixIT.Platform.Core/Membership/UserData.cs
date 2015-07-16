//-----------------------------------------------------------------------
// <copyright file="UserData.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to look up user names in the Cms when querying, as these are stored in another database
    /// which makes it impossible to join them in a query.
    /// </summary>
    public class UserData : ValidationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserData" /> class.
        /// </summary>
        public UserData() { }

        public UserData(Guid userId, string userName, string email)
        {
            this.Id = userId;
            this.Name = userName;
            this.Email = email;
        }

        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        [StrixRequiredWithMembershipAttribute]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [StrixRequiredWithMembershipAttribute]
        [StringLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [StrixRequiredWithMembershipAttribute]
        [StringLength(250)]
        public string Email { get; set; }
    }
}