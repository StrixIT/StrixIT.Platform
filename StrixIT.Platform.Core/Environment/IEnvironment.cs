#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="StrixIT">
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

using StrixIT.Platform.Core.Environment;
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to abstract away the environment the application is running in.
    /// </summary>
    public interface IEnvironment
    {
        #region Public Properties

        bool CmsActive { get; }
        IConfiguration Configuration { get; }
        ICultureService Cultures { get; }
        IMembershipSettings Membership { get; }
        bool MembershipActive { get; }
        ISessionService Session { get; }

        IUserContext User { get; }

        /// <summary>
        /// Gets the working directory (the application root folder).
        /// </summary>
        string WorkingDirectory { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the virtual path for a physical path.
        /// </summary>
        /// <param name="physicalPath">The physical path to get the virtual path for</param>
        /// <returns>The virtual path</returns>
        string GetVirtualPath(string physicalPath);

        /// <summary>
        /// Maps a path to the environment.
        /// </summary>
        /// <param name="path">The path to map</param>
        /// <returns>The mapped path</returns>
        string MapPath(string path);

        #endregion Public Methods
    }
}