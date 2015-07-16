//-----------------------------------------------------------------------
// <copyright file="IValidationBase.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The basic interface for all entities.
    /// </summary>
    public interface IValidationBase : IValidatableObject
    {
        /// <summary>
        /// Gets a value indicating whether the entity is valid.
        /// </summary>
        bool IsValid { get; }
    }
}
