#region Apache License

//-----------------------------------------------------------------------
// <copyright file="UserData.cs" company="StrixIT">
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
using System.ComponentModel.DataAnnotations;

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
        public UserData()
        {
        }

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