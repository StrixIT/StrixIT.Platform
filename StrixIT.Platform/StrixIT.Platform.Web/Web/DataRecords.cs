//-----------------------------------------------------------------------
// <copyright file="DataRecords.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A generic wrapper to pass data from the server to the client.
    /// </summary>
    public class DataRecords
    {
        public DataRecords(IEnumerable data) : this(data, null) { }

        public DataRecords(IEnumerable data, FilterOptions options)
        {
            this.Data = data;
            this.Options = options;
            this.Total = options != null ? options.Total : data.Length();
        }

        /// <summary>
        /// Gets or sets the list of entities to wrap in this data records object.
        /// </summary>
        public IEnumerable Data { get; set; }

        /// <summary>
        /// Gets the total number of records available in the source.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Gets the search options that will be send back to the client.
        /// </summary>
        public FilterOptions Options { get; private set; }
    }
}
