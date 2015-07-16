//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class HomeController : BaseController
    {
        private static bool _startup = true;
        private static object _lockObject = new object();
        private IResourceService _resourceService;

        public HomeController(IResourceService resourceService)
        {
            this._resourceService = resourceService;
        }

        public ActionResult Index()
        {
            this.WriteMessagesOnStartup();
            
            if (StrixPlatform.Configuration.SecureHomeController && !this.HttpContext.Request.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account", new { area = "Membership", culture = StrixPlatform.CurrentCultureCode });
            }

            return this.View();
        }

        [HttpPost]
        public JsonResult GetEnumerations(string moduleName)
        {
            return this.Json(this._resourceService.GetEnums(moduleName));
        }

        [HttpPost]
        public JsonResult GetResources(string moduleName)
        {
            return this.Json(this._resourceService.GetResx(moduleName));
        }

        public ActionResult Error()
        {
            Response.StatusCode = 200;
            Response.TrySkipIisCustomErrors = true;
            return this.View();
        }

        private void WriteMessagesOnStartup()
        {
            if (_startup)
            {
                lock (_lockObject)
                {
                    if (_startup)
                    {
                        StrixPlatform.WriteStartupMessage("Load home page");

                        foreach (var message in StrixPlatform.StartupMessages)
                        {
                            Logger.Log("Application startup: " + message);
                        }

                        _startup = false;
                    }
                }
            }
        }
    }
}