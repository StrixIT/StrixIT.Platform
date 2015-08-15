#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ImageAttribute.cs" company="StrixIT">
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    public static class ValidationExtensions
    {
        #region Public Methods

        public static bool Validate<T>(this T entity)
        {
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(entity, new ValidationContext(entity), results, true);
        }

        public static bool Validate<T>(this T entity, out IList<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(entity, new ValidationContext(entity), results, true);
        }

        public static bool Validate<T>(this IEnumerable<T> list)
        {
            var valid = true;
            var results = new List<ValidationResult>();

            foreach (var entity in list)
            {
                valid = Validator.TryValidateObject(list, new ValidationContext(list), results, true);

                if (!valid)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Validate<T>(this IEnumerable<T> list, out IDictionary<object, IList<ValidationResult>> results)
        {
            var valid = true;
            results = new Dictionary<object, IList<ValidationResult>>();

            foreach (var entity in list)
            {
                var result = new List<ValidationResult>();
                valid = Validator.TryValidateObject(list, new ValidationContext(list), result, true);
                results.Add(entity, result);
            }

            return valid;
        }

        #endregion Public Methods
    }
}