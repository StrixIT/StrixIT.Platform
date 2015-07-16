//-----------------------------------------------------------------------
// <copyright file="StrixNotDefaultWithMembershipAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Verifies that a value type property does not have the default value for its type when membership is active.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StrixNotDefaultWithMembershipAttribute : ValidationAttribute
    {
        private static bool _membershipPresent = DependencyInjector.TryGet<IMembershipService>() != null;

        public override bool IsValid(object value)
        {
            bool result = true;

            if (value == null)
            {
                return result;
            }

            return _membershipPresent ? !Helpers.IsDefaultValue(value) : true;
        }
    }
}