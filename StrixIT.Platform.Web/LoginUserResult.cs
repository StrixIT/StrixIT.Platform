#region Apache License
//-----------------------------------------------------------------------
// <copyright file="LoginUserResult.cs" company="StrixIT">
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
#endregion

namespace StrixIT.Platform.Web
{
    public class LoginUserResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the login was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message to display when the login was not successful.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the preferred culture of the user to use after logging in.
        /// </summary>
        public string PreferredCulture { get; set; }
    }
}
