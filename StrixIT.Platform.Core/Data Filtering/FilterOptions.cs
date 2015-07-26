#region Apache License
//-----------------------------------------------------------------------
// <copyright file="FilterOptions.cs" company="StrixIT">
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The class used to filter, sort and page an enumerable.
    /// </summary>
    public class FilterOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterOptions" /> class.
        /// </summary>
        public FilterOptions()
        {
            this.Sort = new List<SortField>();
            this.Filter = new Filter();
            this.TraverseListPropertyName = "Id";
        }

        /// <summary>
        /// Gets or sets the size of a page to use for paging. When 0, paging is disabled.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the number of the current page to use for paging. When 0, paging is disabled.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public Filter Filter { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of fields to sort on.
        /// </summary>
        public IList<SortField> Sort { get; set; }

        /// <summary>
        /// Gets or sets the value of the total number of records when, sorting and paging is completed. It is set automatically to
        /// contain the total number of records available that satisfy the filter.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include a traverse list.
        /// </summary>
        public bool IncludeTraverseList { get; set; }

        /// <summary>
        /// Gets or sets the name of the id property to use to create the traverse list.
        /// </summary>
        public string TraverseListPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the traverse list.
        /// </summary>
        public IList<TraverseList> TraverseList { get; set; }

        /// <summary>
        /// Removes a field from the filter and returns it. This must be used when custom handling of the field is required, before the
        /// general filter code is executed.
        /// </summary>
        /// <param name="name">The name of the field to extract</param>
        /// <returns>The filter field</returns>
        public FilterField ExtractField(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var field = this.Filter.Filters.FirstOrDefault(f => f.Field.ToLower() == name.ToLower());

            if (field != null)
            {
                this.Filter.Filters.Remove(field);
                return field;
            }

            return null;
        }
    }
}