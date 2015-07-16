//-----------------------------------------------------------------------
// <copyright file="PageController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.IO;
using System.Linq;
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The controller for custom pages that have no controller themselves. This controller is used to allow users to add
    /// views to the Cms Views folder without having a controller for them. They can just add a .cshtml file, and add widgets
    /// to it using razor statements without the need to compile the application or use Visual Studio.
    /// </summary>
    public class PageController : BaseController
    {
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

            return this.View("~/" + Web.Helpers.GetVirtualPath(viewPath));
        }
    }
}