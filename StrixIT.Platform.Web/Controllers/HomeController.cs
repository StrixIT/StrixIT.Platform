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
        #region Private Fields

        private IResourceService _resourceService;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(IEnvironment environment, IResourceService resourceService) : base(environment)
        {
            _resourceService = resourceService;
        }

        #endregion Public Constructors

        #region Public Methods

        public ActionResult Error()
        {
            Response.StatusCode = 200;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        [HttpPost]
        public JsonResult GetEnumerations(string moduleName)
        {
            return Json(_resourceService.GetEnums(moduleName));
        }

        [HttpPost]
        public JsonResult GetResources(string moduleName)
        {
            return Json(_resourceService.GetResx(moduleName));
        }

        public ActionResult Index()
        {
            if (Environment.Configuration.GetConfiguration<PlatformConfiguration>().SecureHomeController && !HttpContext.Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Membership", culture = Environment.Cultures.CurrentCultureCode });
            }

            return View();
        }

        #endregion Public Methods
    }
}