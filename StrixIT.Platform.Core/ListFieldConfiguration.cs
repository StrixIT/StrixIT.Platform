#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ListFieldConfiguration.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to configure a field for a list.
    /// </summary>
    public class ListFieldConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListFieldConfiguration" /> class.
        /// </summary>
        /// <param name="name">The field name</param>
        public ListFieldConfiguration(string name) : this(name, null, FilterFieldOperator.Contains) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListFieldConfiguration" /> class.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="filterName">The name of the filter to render the field</param>
        public ListFieldConfiguration(string name, string filterName) : this(name, filterName, FilterFieldOperator.Contains) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListFieldConfiguration" /> class.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="filterName">The name of the filter to render the field</param>
        /// <param name="fieldOperator">The filter operator to use for the field</param>
        public ListFieldConfiguration(string name, string filterName, FilterFieldOperator fieldOperator)
        {
            this.Name = name;
            this.FilterName = filterName;
            this.Operator = fieldOperator;
            this.ShowFilter = true;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the field filter name. This is the name of the angular filter to run when displaying the field.
        /// </summary>
        public string FilterName { get; private set; }

        /// <summary>
        /// Gets the operator to use for the field.
        /// </summary>
        public FilterFieldOperator Operator { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether a filter should be shown in the list for this field.
        /// </summary>
        public bool ShowFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether html values for this field should be displayed as such.
        /// </summary>
        public bool DisplayHtml { get; set; }
    }
}
