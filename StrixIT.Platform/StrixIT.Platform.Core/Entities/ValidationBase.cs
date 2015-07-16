//-----------------------------------------------------------------------
// <copyright file="ValidationBase.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The base class for all entities.
    /// </summary>
    public abstract class ValidationBase : IValidationBase
    {
        /// <summary>
        /// The custom validation rules
        /// </summary>
        private static ConcurrentDictionary<Type, List<Func<ValidationBase, ValidationResult>>> _customValidationRules = new ConcurrentDictionary<Type, List<Func<ValidationBase, ValidationResult>>>();

        /// <summary>
        /// The list of all validation results from the last validation run.
        /// </summary>
        private List<ValidationResult> _validationResults = new List<ValidationResult>();

        /// <summary>
        /// True if the entity is currently validating, false otherwise.
        /// </summary>
        private bool _isValidating = false;

        /// <summary>
        /// Gets a value indicating whether the entity is valid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                return this.Validate(null).Count() == 0;
            }
        }

        /// <summary>
        /// Adds the validation rule.
        /// </summary>
        /// <typeparam name="T">The type of the object to add the validation rule to.</typeparam>
        /// <param name="rule">The validation rule.</param>
        public static void AddValidationRule<T>(Func<T, ValidationResult> rule) where T : ValidationBase
        {
            var type = typeof(T);

            if (!_customValidationRules.ContainsKey(type))
            {
                _customValidationRules.GetOrAdd(type, new List<Func<ValidationBase, ValidationResult>>());
            }

            Func<ValidationBase, ValidationResult> ruleToAdd = x => rule(x as T);
            _customValidationRules[type].Add(ruleToAdd);
        }

        /// <summary>
        /// Validates the entity using the specified validation context. Override this method to add custom validation rules.
        /// </summary>
        /// <param name="validationContext">The context to use for validation</param>
        /// <returns>The list of validation errors, or an empty list if the entity is valid</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!this._isValidating)
            {
                this._isValidating = true;
                this._validationResults.Clear();
                Validator.TryValidateObject(this, validationContext != null ? validationContext : new ValidationContext(this), this._validationResults, true);
                List<Func<ValidationBase, ValidationResult>> customRules;

                if (_customValidationRules.TryGetValue(this.GetType(), out customRules))
                {
                    foreach (var rule in customRules)
                    {
                        var result = rule(this);

                        if (result != null)
                        {
                            this._validationResults.Add(result);
                        }
                    }
                }

                this._isValidating = false;
            }

            return this._validationResults;
        }
    }
}