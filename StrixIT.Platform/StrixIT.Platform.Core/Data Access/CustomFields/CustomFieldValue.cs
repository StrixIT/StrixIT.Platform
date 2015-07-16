//-----------------------------------------------------------------------
// <copyright file="CustomFieldValue.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to define custom field values.
    /// </summary>
    /// <typeparam name="T">The custom field type for this value class</typeparam>
    public abstract class CustomFieldValue<T> where T : CustomField
    {
        /// <summary>
        /// Gets or sets the value id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the custom field id for this custom field value.
        /// </summary>
        [StrixRequired]
        public Guid CustomFieldId { get; set; }

        /// <summary>
        /// Gets or sets the custom field for this custom field value.
        /// </summary>
        public T CustomField { get; set; }

        /// <summary>
        /// Gets or sets the custom field number value.
        /// </summary>
        public double? NumberValue { get; set; }

        /// <summary>
        /// Gets or sets the custom field string value.
        /// </summary>
        public string StringValue { get; set; }
    }
}
