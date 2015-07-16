//-----------------------------------------------------------------------
// <copyright file="TraverseList.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to walk through a list of entities.
    /// </summary>
    [Serializable]
    public class TraverseList
    {
        /// <summary>
        /// Gets or sets the id of the entity previous to this one.
        /// </summary>
        public object PreviousId { get; set; }

        /// <summary>
        /// Gets or sets the id of the current entity.
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the entity next to this one.
        /// </summary>
        public object NextId { get; set; }
    }
}
