#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ListConfiguration.cs" company="StrixIT">
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
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to configure the data lists for the platform.
    /// </summary>
    public class ListConfiguration
    {
        private IList<ListFieldConfiguration> _fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListConfiguration" /> class.
        /// </summary>
        /// <param name="dtoType">The dto type the list is for</param>
        public ListConfiguration(Type dtoType) : this(dtoType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListConfiguration" /> class.
        /// </summary>
        /// <param name="dtoType">The dto type the list is for</param>
        /// <param name="propertyNames">The names of the properties to use in the list</param>
        public ListConfiguration(Type dtoType, IEnumerable<string> propertyNames)
        {
            this.DtoType = dtoType;
            this.TypeName = dtoType.Name.ToLower().Replace("listmodel", string.Empty).Replace("viewmodel", string.Empty).Replace("dto", string.Empty);
            this.InterfaceResourceType = typeof(Resources.DefaultInterface);
            this._fields = new List<ListFieldConfiguration>();

            if (propertyNames != null)
            {
                foreach (var property in propertyNames)
                {
                    var propertyType = dtoType.GetProperties().First(p => p.Name.ToLower() == property.ToLower()).PropertyType;
                    propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    var propertyName = propertyType == typeof(DateTime) ? "kendoDate" : null;
                    this._fields.Add(new ListFieldConfiguration(property, propertyName));
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the resource file to localize the list.
        /// </summary>
        public Type InterfaceResourceType { get; set; }

        /// <summary>
        /// Gets or sets the dto type the list is for.
        /// </summary>
        public Type DtoType { get; set; }

        /// <summary>
        /// Gets or sets the name of the type te configuration is for.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the name column should be hidden.
        /// </summary>
        public bool HideNameColumn { get; set; }

        /// <summary>
        /// Gets the list fields.
        /// </summary>
        public IList<ListFieldConfiguration> Fields
        {
            get
            {
                return this._fields;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can create objects for this list.
        /// </summary>
        public bool CanCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can edit objects in this list.
        /// </summary>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can delete objects in this list.
        /// </summary>
        public bool CanDelete { get; set; }
    }
}