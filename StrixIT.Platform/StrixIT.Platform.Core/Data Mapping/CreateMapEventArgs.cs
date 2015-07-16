//-----------------------------------------------------------------------
// <copyright file="CreateMapEventArgs.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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