//-----------------------------------------------------------------------
// <copyright file="JsonValidateAntiForgeryTokenAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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