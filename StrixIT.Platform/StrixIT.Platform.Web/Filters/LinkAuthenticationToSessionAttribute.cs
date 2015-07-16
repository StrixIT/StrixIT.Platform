//-----------------------------------------------------------------------
// <copyright file="LinkAuthenticationToSessionAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// Link the .Net authentication to the session to prevent session fixation.
    /// Adopted from http://blog.securityps.com/2013/06/session-fixation-forms-authentication.html.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LinkAuthenticationToSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.Request.IsLocal)
            {
                var email = (string)filterContext.HttpContext.Session[PlatformConstants.CURRENTUSEREMAIL];

                // If the user is authenticated, compare the email in the session and forms auth cookie. If they don't match, logoff the user,
                // kill the session and redirect to the login page.
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
    }
}