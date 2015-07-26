#region Apache License

//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="StrixIT">
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