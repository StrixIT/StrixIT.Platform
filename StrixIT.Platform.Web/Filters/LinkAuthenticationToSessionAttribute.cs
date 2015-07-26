#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LinkAuthenticationToSessionAttribute.cs" company="StrixIT">
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
using System.Web.Mvc;
using System.Web.Routing;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// Link the .Net authentication to the session to prevent session fixation. Adopted from http://blog.securityps.com/2013/06/session-fixation-forms-authentication.html.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LinkAuthenticationToSessionAttribute : ActionFilterAttribute
    {
        #region Public Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.Request.IsLocal)
            {
                var email = (string)filterContext.HttpContext.Session[PlatformConstants.CURRENTUSEREMAIL];

                // If the user is authenticated, compare the email in the session and forms auth
                // cookie. If they don't match, logoff the user, kill the session and redirect to
                // the login page.
                if (filterContext.HttpContext.User.Identity.IsAuthenticated || email != null)
                {
                    if (email == null || email != filterContext.HttpContext.User.Identity.Name)
                    {
                        if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.Name.ToLower() != "accountcontroller" || filterContext.ActionDescriptor.ActionName.ToLower() != "login")
                        {
                            var service = DependencyInjector.TryGet<IAuthenticationService>();

                            if (service != null)
                            {
                                service.LogOff(email);
                            }

                            filterContext.HttpContext.Session.Abandon();

                            // if the request is an ajax request, set the status code and end the response
                            if (AjaxRequestExtensions.IsAjaxRequest(filterContext.HttpContext.Request))
                            {
                                filterContext.Result = new HttpStatusCodeResult(401);
                                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                                return;
                            }

                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { MvcConstants.AREA, string.Empty }, { MvcConstants.ACTION, "login" }, { MvcConstants.CONTROLLER, "account" }, { "returnurl", filterContext.HttpContext.Request.Url.PathAndQuery } });
                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        #endregion Public Methods
    }
}