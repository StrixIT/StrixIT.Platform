﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="StrixAuthorizationAttribute.cs" company="StrixIT">
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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A custom autorization attribute for authorization of MVC controllers using StrixIT
    /// membership functionality.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class StrixAuthorizationAttribute : AuthorizeAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the permissions that the role a user is part of needs to have one or more
        /// of to be allowed access.
        /// </summary>
        public string Permissions { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!SkipAuthorization(filterContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                cache.SetProxyMaxAge(new TimeSpan(0L));
                cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), (object)null);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            // Use permissions when configured.
            if (!string.IsNullOrWhiteSpace(this.Permissions))
            {
                var allowedPermissions = this.Permissions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Trim();
                return StrixPlatform.User.HasPermission(allowedPermissions);
            }

            // Use roles when configured.
            if (!string.IsNullOrWhiteSpace(this.Roles))
            {
                var allowedRoles = this.Roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Trim();
                return StrixPlatform.User.IsInRoles(allowedRoles);
            }

            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            // if ajax request set status code and end responcse
            if (AjaxRequestExtensions.IsAjaxRequest(filterContext.HttpContext.Request))
            {
                filterContext.Result = new HttpStatusCodeResult(401);
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }

        #endregion Protected Methods

        #region Private Methods

        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }

        /// <summary>
        /// A handler called on cache validation, to integrate authorization with caching for
        /// securing files on the file system.
        /// </summary>
        /// <param name="context">The http context</param>
        /// <param name="data">the data</param>
        /// <param name="validationStatus">the http validation status</param>
        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = this.OnCacheAuthorization((HttpContextBase)new HttpContextWrapper(context));
        }

        #endregion Private Methods
    }
}