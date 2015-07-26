#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DataRecords.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System.Collections;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A generic wrapper to pass data from the server to the client.
    /// </summary>
    public class DataRecords
    {
        public DataRecords(IEnumerable data) : this(data, null)
        {
        }

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