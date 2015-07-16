//-----------------------------------------------------------------------
// <copyright file="CustomField.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Defines a custom field that can be added to a variety of objects.
    /// </summary>
    public class CustomField
    {
        /// <summary>
        /// Gets or sets the custom field id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the custom field type.
        /// </summary>
        public CustomFieldType FieldType { get; set; }

        /// <summary>
        /// Gets or sets the custom field name.
        /// </summary>
        [StrixRequired]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the section the custom field belongs to.
        /// </summary>
        [StringLength(100)]
        public string Section { get; set; }
    }
}
