//-----------------------------------------------------------------------
// <copyright file="ModifiedPropertyValue.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to track property value changes.
    /// </summary>
    public class ModifiedPropertyValue
    {
        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        public object NewValue { get; set; }
    }
}
