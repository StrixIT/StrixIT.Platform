//-----------------------------------------------------------------------
// <copyright file="SortField.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The class for sort fields used with the filter options.
    /// </summary>
    public class SortField
    {
        /// <summary>
        /// Gets or sets the name of the property to sort on.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the direction for the sort (Asc or Desc).
        /// </summary>
        public string Dir { get; set; }
    }
}
