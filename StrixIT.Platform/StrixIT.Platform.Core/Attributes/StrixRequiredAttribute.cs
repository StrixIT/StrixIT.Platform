﻿#region Apache License
//-----------------------------------------------------------------------
// <copyright file="StrixRequiredAttribute.cs" company="StrixIT">
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