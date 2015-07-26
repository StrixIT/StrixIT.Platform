#region Apache License

//-----------------------------------------------------------------------
// <copyright file="JsonValidateAntiForgeryTokenAttribute.cs" company="StrixIT">
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

using System;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An attribute to validate antiforgery tokens for JSON calls as well as form posts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class JsonValidateAntiForgeryTokenAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (request.IsAuthenticated && request.HttpMethod != WebRequestMethods.Http.Get)
            {
                // Ajax POSTs and normal form posts have to be treated differently when it comes
                // to validating the AntiForgeryToken
                if (request.IsAjaxRequest())
                {
                    HttpCookie antiForgeryCookie = null;

                    foreach (var key in request.Cookies.Keys)
                    {
                        if (key is string && ((string)key).Contains(AntiForgeryConfig.CookieName))
                        {
                            antiForgeryCookie = request.Cookies[(string)key];
                        }
                    }

                    var cookieValue = antiForgeryCookie != null ? antiForgeryCookie.Value : null;
                    AntiForgery.Validate(cookieValue, request.Headers[AntiForgeryConfig.CookieName]);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
        }
    }
}