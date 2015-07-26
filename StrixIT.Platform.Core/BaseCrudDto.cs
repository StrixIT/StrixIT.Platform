#region Apache License

//-----------------------------------------------------------------------
// <copyright file="BaseCrudDto.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The base class for CRUD dtos.
    /// </summary>
    public class BaseCrudDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCrudDto" /> class.
        /// </summary>
        /// <param name="dtoType">The entity type the dto is for</param>
        public BaseCrudDto(Type dtoType)
        {
            this.EntityType = dtoType;
            this.InterfaceResourceType = typeof(Resources.DefaultInterface);
        }

        /// <summary>
        /// Gets or sets the type of the resource file to localize the interface.
        /// </summary>
        public Type InterfaceResourceType { get; set; }

        /// <summary>
        /// Gets or sets the entity type the dto is for.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can edit this entity.
        /// </summary>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can delete this entity.
        /// </summary>
        public bool CanDelete { get; set; }
    }
}