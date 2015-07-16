//-----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StrixIT.Platform.Core;

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