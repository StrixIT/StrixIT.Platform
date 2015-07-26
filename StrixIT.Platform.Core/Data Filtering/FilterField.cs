#region Apache License

//-----------------------------------------------------------------------
// <copyright file="FilterField.cs" company="StrixIT">
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
    /// This class represents a field used in filtering lists of entities.
    /// </summary>
    public class FilterField
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField"/> class. The operator is set
        /// to "Equals".
        /// </summary>
        public FilterField() : this(FilterFieldOperator.Equals, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField"/> class.
        /// </summary>
        /// <param name="filterOperator">The operator to use for filtering</param>
        public FilterField(FilterFieldOperator filterOperator) : this(filterOperator, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField"/> class.
        /// </summary>
        /// <param name="filterOperator">The operator to use for filtering</param>
        /// <param name="name">The field name</param>
        /// <param name="value">The field value</param>
        public FilterField(FilterFieldOperator filterOperator, string name, string value)
        {
            this.Operator = filterOperator;
            this.Field = name ?? string.Empty;
            this.Value = value;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the property to filter on.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the method to use for filtering.
        /// </summary>
        public FilterFieldOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value of the filter.
        /// </summary>
        public string Value { get; set; }

        #endregion Public Properties
    }
}