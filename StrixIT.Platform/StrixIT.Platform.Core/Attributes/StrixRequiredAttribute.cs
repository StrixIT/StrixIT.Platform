//-----------------------------------------------------------------------
// <copyright file="StrixRequiredAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Verifies that a property is not null, that a value type property does not have the default value for its type, and that a string property
    /// is not empty or whitespace.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StrixRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var isValid = base.IsValid(value);

            if (base.IsValid(value))
            {
                Type objectType = value.GetType();

                if (objectType.IsValueType)
                {
                    isValid = !Helpers.IsDefaultValue(value);
                }
                else if (objectType.Equals(typeof(string)))
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    { 
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}