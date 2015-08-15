#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IMembershipService.cs" company="StrixIT">
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
using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface for the membership service.
    /// </summary>
    public interface IMembershipService
    {
        #region Public Properties

        /// <summary>
        /// Gets the id of the admin user.
        /// </summary>
        Guid AdminId { get; }

        /// <summary>
        /// Gets the id of the application.
        /// </summary>
        Guid ApplicationId { get; }

        /// <summary>
        /// Gets the id of the main group.
        /// </summary>
        Guid MainGroupId { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets a query of group data.
        /// </summary>
        /// <returns>The group data</returns>
        IQueryable<GroupData> GroupData();

        /// <summary>
        /// Initializes the membership service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets a query of user data.
        /// </summary>
        /// <returns>The user data</returns>
        IQueryable<UserData> UserData();

        #endregion Public Methods
    }
}