//-----------------------------------------------------------------------
// <copyright file="ListFieldConfiguration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
