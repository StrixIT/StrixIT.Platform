#region Apache License

//-----------------------------------------------------------------------
// <copyright file="StrixPlatformBinder.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A binder to prevent XSS attacks with JSON and register custom bindings.
    /// </summary>
    public class StrixPlatformBinder : DefaultModelBinder
    {
        private static char[] startingChars = new char[] { '<', '&' };

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object result = null;
            var args = new BindModelEvent(controllerContext, bindingContext);
            StrixPlatform.RaiseEvent(args);

            if (args.IsBound)
            {
                result = args.Result;
            }
            else
            {
                result = base.BindModel(controllerContext, bindingContext);

                if (bindingContext.ModelMetadata.Container == null && result != null && result.GetType().Equals(typeof(string)))
                {
                    if (controllerContext.Controller.ValidateRequest)
                    {
                        int index;

                        if (IsDangerousString((string)result, out index))
                        {
                            throw new HttpRequestValidationException("Dangerous Input Detected");
                        }
                    }

                    result = GetSafeValue((string)result);
                }
            }

            return result;
        }

        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            if (value != null && value.GetType().Equals(typeof(string)))
            {
                if (controllerContext.Controller.ValidateRequest && bindingContext.PropertyMetadata[propertyDescriptor.Name].RequestValidationEnabled)
                {
                    value = GetSafeValue((string)value);
                }
            }

            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            if (propertyDescriptor == null)
            {
                throw new ArgumentNullException("propertyDescriptor");
            }

            if (value is string && controllerContext.HttpContext.Request.ContentType.StartsWith(WebConstants.APPLICATIONJSON, StringComparison.OrdinalIgnoreCase))
            {
                if (controllerContext.Controller.ValidateRequest && bindingContext.PropertyMetadata[propertyDescriptor.Name].RequestValidationEnabled)
                {
                    int index;

                    if (IsDangerousString(value.ToString(), out index))
                    {
                        throw new HttpRequestValidationException("Dangerous Input Detected");
                    }
                }
            }

            return base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
        }

        private static string GetSafeValue(string value)
        {
            if (value == "/")
            {
                return value;
            }

            return Helpers.HtmlDecode((string)value);
        }

        private static bool IsAtoZ(char c)
        {
            return ((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z'));
        }

        private static bool IsDangerousString(string s, out int matchIndex)
        {
            // Remove newline characters left by Angular's input sanitation.
            s = HttpUtility.HtmlDecode(s).Replace("\n", string.Empty);

            matchIndex = 0;
            int startIndex = 0;
            while (true)
            {
                int num2 = s.IndexOfAny(startingChars, startIndex);
                if (num2 < 0)
                {
                    return false;
                }

                if (num2 == (s.Length - 1))
                {
                    return false;
                }

                matchIndex = num2;
                char ch = s[num2];

                if (ch != '&')
                {
                    if ((ch == '<') && ((IsAtoZ(s[num2 + 1]) || (s[num2 + 1] == '!')) || ((s[num2 + 1] == '/') || (s[num2 + 1] == '?'))))
                    {
                        return true;
                    }
                }
                else if (s[num2 + 1] == '#')
                {
                    return true;
                }

                startIndex = num2 + 1;
            }
        }
    }
}