//-----------------------------------------------------------------------
// <copyright file="BaseCrudDto.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

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