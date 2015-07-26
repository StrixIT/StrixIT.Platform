#region Apache License
//-----------------------------------------------------------------------
// <copyright file="CustomFieldValue.cs" company="StrixIT">
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
