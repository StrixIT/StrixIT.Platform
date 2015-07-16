//-----------------------------------------------------------------------
// <copyright file="Filter.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The filter class used to filter data.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter" /> class.
        /// </summary>
        public Filter()
        {
            this.Logic = FilterType.And;
            this.Filters = new List<FilterField>();
        }

        /// <summary>
        /// Gets or sets the filter logic (And or Or).
        /// </summary>
        public FilterType Logic { get; set; }

        /// <summary>
        /// Gets or sets the filters to use.
        /// </summary>
        public IList<FilterField> Filters { get; set; }
    }
}
