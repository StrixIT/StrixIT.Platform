#region Apache License

//-----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="StrixIT">
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
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The base class for StrixIT Platform controllers.
    /// </summary>
    public class BaseController : Controller
    {
        #region Private Fields

        private IEnvironment _environment;

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController(IEnvironment environment)
        {
            _environment = environment;

            // Set the culture for the controller.
            var cultureCode = _environment.Cultures.CurrentCultureCode;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureCode);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);
            }
        }

        #endregion Protected Constructors

        #region Protected Methods

        protected IEnvironment Environment
        {
            get
            {
                return _environment;
            }
        }

        /// <summary>
        /// Gets the view result for the requested view or the template in case of a non-ajax
        /// request. This is needed to support refreshing the browser at any point in a SPA.
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
        /// Gets the view result for the requested view or the template in case of a non-ajax
        /// request. This is needed to support refreshing the browser at any point in a SPA.
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

        #endregion Protected Methods

        #region Private Methods

        private bool IsTemplate()
        {
            return !this.ControllerContext.HttpContext.Request.IsAjaxRequest() && !ControllerContext.IsChildAction && this.ControllerContext.HttpContext.Request.Url.AbsolutePath.ToLower().Contains("/admin/");
        }

        #endregion Private Methods
    }
}