//-----------------------------------------------------------------------
// <copyright file="StrixNotDefaultAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Verifies that a value type property does not have the default value for its type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StrixNotDefaultAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;

            if (value == null)
            {
                return result;
            }

            return !Helpers.IsDefaultValue(value);
        }
    }
}