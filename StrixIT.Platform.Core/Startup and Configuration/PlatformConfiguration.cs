#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PlatformConfigurationSection.cs" company="StrixIT">
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
    /// The class for configuring the StrixIT platform.
    /// </summary>
    public class PlatformConfiguration
    {
        #region Public Constructors

        public PlatformConfiguration()
        {
            Cultures = "en";
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the cultures supported in the application.
        /// </summary>
        public string Cultures { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the home controller of the application should be
        /// accessible only by authenticated users.
        /// </summary>
        public bool SecureHomeController { get; set; }

        #endregion Public Properties
    }
}