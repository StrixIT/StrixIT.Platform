#region Apache License

//-----------------------------------------------------------------------
// <copyright file="CustomField.cs" company="StrixIT">
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