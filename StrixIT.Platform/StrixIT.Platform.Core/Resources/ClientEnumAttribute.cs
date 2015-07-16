//-----------------------------------------------------------------------
// <copyright file="ClientEnumAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An attribute to mark an enum as an enum that should be loaded to the client.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ClientEnumAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the resource to use to localize the enum.
        /// </summary>
        public Type ResourceType { get; set; }
    }
}
