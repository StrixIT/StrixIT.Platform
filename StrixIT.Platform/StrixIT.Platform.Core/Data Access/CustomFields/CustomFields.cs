//-----------------------------------------------------------------------
// <copyright file="CustomFields.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A helper class to use custom fields.
    /// </summary>
    public static class CustomFields
    {
        /// <summary>
        /// Gets a list of custom field values, grouped by the specified property (e.g. userid to group the values by user
        /// in case of profile values).
        /// </summary>
        /// <typeparam name="TType">The custom field type</typeparam>
        /// <typeparam name="TValue">The custom field value type</typeparam>
        /// <param name="query">The custom field value query</param>
        /// <param name="groupPropertyName">The property to group the custom field values by</param>
        /// <returns>The list of custom field values</returns>
        public static IList<dynamic> GetCustomFieldsList<TType, TValue>(IQueryable<TValue> query, string groupPropertyName)
            where TType : CustomField
            where TValue : CustomFieldValue<TType> 
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (string.IsNullOrWhiteSpace(groupPropertyName))
            {
                throw new ArgumentNullException("groupPropertyName");
            }

            var customObjects = new List<dynamic>();

            foreach (var customFields in query.GroupBy(groupPropertyName, "it"))
            {
                var customObject = new ExpandoObject() as IDictionary<string, object>;

                foreach (TValue property in customFields as IEnumerable)
                {
                    switch (property.CustomField.FieldType)
                    {
                        case CustomFieldType.Integer:
                            {
                                customObject[property.CustomField.Name] = Convert.ToInt64(property.NumberValue);
                            }
 
                            break;
                        case CustomFieldType.Float:
                            {
                                customObject[property.CustomField.Name] = property.NumberValue;
                            } 

                            break;
                        case CustomFieldType.DateTime:
                            {
                                customObject[property.CustomField.Name] = new DateTime(Convert.ToInt64(property.NumberValue));
                            } 

                            break;
                        case CustomFieldType.Boolean:
                            {
                                customObject[property.CustomField.Name] = Convert.ToBoolean(property.NumberValue);
                            } 

                            break;
                        case CustomFieldType.String:
                            {
                                customObject[property.CustomField.Name] = property.StringValue;
                            }

                            break;
                    }
                }

                customObjects.Add(customObject);
            }

            return customObjects;
        }
    }
}