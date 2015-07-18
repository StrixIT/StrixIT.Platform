#region Apache License
//-----------------------------------------------------------------------
// <copyright file="CreateMapEventArgs.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Event arguments for when a map is created by the data mapper.
    /// </summary>
    public class CreateMapEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMapEventArgs" /> class.
        /// </summary>
        /// <param name="sourceType">The source type of the map</param>
        /// <param name="destinationType">The destination type of the map</param>
        public CreateMapEventArgs(Type sourceType, Type destinationType)
        {
            this.Types = new KeyValuePair<Type, Type>(sourceType, destinationType);
            this.PropertiesToIgnore = new List<string>();
            this.PropertiesToMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the source and destination types of the map.
        /// </summary>
        public KeyValuePair<Type, Type> Types { get; private set; }

        /// <summary>
        /// Gets or sets the list of properties to ignore when creating the new map.
        /// </summary>
        public IList<string> PropertiesToIgnore { get; set; }

        /// <summary>
        /// Gets or sets the list of destination and source properties that should be mapped to each other when the map is created.
        /// </summary>
        public IDictionary<string, string> PropertiesToMap { get; set; }
    }
}