//-----------------------------------------------------------------------
// <copyright file="FilterField.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// This class represents a field used in filtering lists of entities.
    /// </summary>
    public class FilterField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField" /> class.
        /// The operator is set to "Equals".
        /// </summary>
        public FilterField() : this(FilterFieldOperator.Equals, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField" /> class.
        /// </summary>
        /// <param name="filterOperator">The operator to use for filtering</param>
        public FilterField(FilterFieldOperator filterOperator) : this(filterOperator, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterField" /> class.
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
    }
}
