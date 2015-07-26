#region Apache License

//-----------------------------------------------------------------------
// <copyright file="SortField.cs" company="StrixIT">
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
    /// The class for sort fields used with the filter options.
    /// </summary>
    public class SortField
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the direction for the sort (Asc or Desc).
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// Gets or sets the name of the property to sort on.
        /// </summary>
        public string Field { get; set; }

        #endregion Public Properties
    }
}