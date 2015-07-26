#region Apache License

//-----------------------------------------------------------------------
// <copyright file="FilterSortMap.cs" company="StrixIT">
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
    /// A class to create custom mappings for dynamically filtering and sorting on fields.
    /// </summary>
    public class FilterSortMap
    {
        /// <summary>
        /// Gets or sets the name of the field that this custom map is for
        /// </summary>
        public string FieldToMap { get; set; }

        /// <summary>
        /// Gets or sets the func to use for filtering on this field. Its parameters are the filter method and the filter value.
        /// </summary>
        public Func<FilterFieldOperator, string, string> FilterMap { get; set; }

        /// <summary>
        /// Gets or sets the func to use for filtering on this field. The string value indicates whether to sort ascending
        /// (Asc) or descending (Desc).
        /// </summary>
        public Func<IQueryable, string, IQueryable> SortMap { get; set; }
    }
}