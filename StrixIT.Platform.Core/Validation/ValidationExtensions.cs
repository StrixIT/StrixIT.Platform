using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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