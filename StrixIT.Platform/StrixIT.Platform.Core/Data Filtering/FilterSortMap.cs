//-----------------------------------------------------------------------
// <copyright file="FilterSortMap.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
