#region Apache License

//-----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="StrixIT">
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
using System.Linq;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    [StrixAuthorization(Permissions = PlatformPermissions.ViewAdminDashboard)]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            var modules = ModuleManager.GetObjectList<IModuleConfiguration>();

            if (!modules.Any(m => m.ModuleLinks.Any()))
            {
                Response.StatusCode = 404;
                Response.TrySkipIisCustomErrors = true;
                return this.View("NotFound", "_Layout");
            }

            if (!this.Request.IsAjaxRequest())
            {
                return this.View(MvcConstants.TEMPLATE);
            }

            ViewBag.Modules = modules.Where(m => m.ModuleLinks.Count > 0).OrderBy(e => e.Name);
            return this.View();
        }
    }
}