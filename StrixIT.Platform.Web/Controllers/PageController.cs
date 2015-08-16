#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PageController.cs" company="StrixIT">
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
using System.IO;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The controller for custom pages that have no controller themselves. This controller is used
    /// to allow users to add views to the Cms Views folder without having a controller for them.
    /// They can just add a .cshtml file, and add widgets to it using razor statements without the
    /// need to compile the application or use Visual Studio.
    /// </summary>
    public class PageController : BaseController
    {
        #region Public Constructors

        public PageController(IEnvironment environment) : base(environment)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Renders a custom page, if the url can be resolved. Otherwise, the 404 page is rendered.
        /// </summary>
        /// <param name="url">The url for the custom page</param>
        /// <returns>The View result for the custom page</returns>
        public ActionResult RenderPage(string url)
        {
            string viewPath = null;

            if (url != null)
            {
                string basePath = Server.MapPath("~");
                string physicalPath = "Views\\" + url.Replace("/", "\\");
                string combinedPath = Path.Combine(basePath, physicalPath);

                if (System.IO.File.Exists(combinedPath + ".cshtml"))
                {
                    viewPath = combinedPath + ".cshtml";
                }
                else if (System.IO.File.Exists(combinedPath + "\\Index.cshtml"))
                {
                    viewPath = combinedPath + "\\Index.cshtml";
                }
            }

            if (string.IsNullOrWhiteSpace(viewPath))
            {
                this.Response.StatusCode = 404;
                this.Response.TrySkipIisCustomErrors = true;
                return this.View("NotFound");
            }

            return this.View("~/" + Environment.GetVirtualPath(viewPath));
        }

        #endregion Public Methods
    }
}