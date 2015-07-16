//-----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The base class for StrixIT Platform controllers.
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        protected BaseController()
        {
            // Set the culture for the controller.
            var cultureCode = StrixPlatform.CurrentCultureCode;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureCode);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);
            }
        }

        /// <summary>
        /// Gets the view result for the requested view or the template in case of a non-ajax request. This is needed to support refreshing the browser at
        /// any point in a SPA.
        /// </summary>
        /// <param name="view">The view to get</param>
        /// <param name="model">The model for the view</param>
        /// <returns>The view result</returns>
        protected override ViewResult View(IView view, object model)
        {
            if (this.IsTemplate())
            {
                return base.View(MvcConstants.TEMPLATE, model);
            }

            return base.View(view, model);
        }

        /// <summary>
        /// Gets the view result for the requested view or the template in case of a non-ajax request. This is needed to support refreshing the browser at
        /// any point in a SPA.
        /// </summary>
        /// <param name="viewName">The name of the view to get</param>
        /// <param name="masterName">The name of the master for the view</param>
        /// <param name="model">The model for the view</param>
        /// <returns>The view result</returns>
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (this.IsTemplate())
            {
                return base.View(MvcConstants.TEMPLATE, masterName, model);
            }

            return base.View(viewName, masterName, model);
        }

        private bool IsTemplate()
        {
            return !this.ControllerContext.HttpContext.Request.IsAjaxRequest() && !ControllerContext.IsChildAction && this.ControllerContext.HttpContext.Request.Url.AbsolutePath.ToLower().Contains("/admin/");
        }
    }
}