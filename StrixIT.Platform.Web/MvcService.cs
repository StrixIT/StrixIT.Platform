#region Apache License

//-----------------------------------------------------------------------
// <copyright file="MvcService.cs" company="StrixIT">
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StrixIT.Platform.Web
{
    public class MvcService : IMvcService
    {
        #region Private Fields

        private IEnvironment _environment;

        #endregion Private Fields

        #region Public Constructors

        public MvcService(IEnvironment environment)
        {
            this._environment = environment;
        }

        #endregion Public Constructors

        #region Public Methods

        public void ConfigureRoutes(RouteCollection routes)
        {
            var culture = _environment.Cultures.DefaultCultureCode.ToLower();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.RouteExistingFiles = true;

            var adminRoute = RouteTable.Routes.MapLocalizedRoute(
                 "Admin_default",
                 "{language}/Admin/{controller}/{action}/{id}",
                 new { language = culture, controller = WebConstants.ADMIN, action = MvcConstants.INDEX, id = UrlParameter.Optional },
                 new { controller = new AdminRouteConstraint(_environment.Cultures) });

            routes.RemoveAt(routes.Count - 1);
            routes.Insert(0, adminRoute);

            routes.MapLocalizedRoute(
                "NotFound",
                "{language}/NotFound/{url}",
                new { language = culture, controller = "Home", action = "NotFound", url = UrlParameter.Optional });

            routes.MapLocalizedRoute(
                "Error",
                "{language}/Error",
                new { language = culture, controller = "Home", action = "Error" });

            routes.MapLocalizedRoute(
                name: "Search",
                url: "{language}/Search/{action}/{options}",
                defaults: new { language = culture, controller = "Search", action = "Index", options = UrlParameter.Optional });

            routes.MapLocalizedRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { language = culture, controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        public void Initialize()
        {
            this.ConfigureViewEngines();
            this.ConfigureBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();

            this.ConfigureRoutes(RouteTable.Routes);

            this.ConfigureFilters();

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());
            ModelBinders.Binders.DefaultBinder = new StrixPlatformBinder();
            MvcHandler.DisableMvcResponseHeader = true;

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }

        #endregion Public Methods

        #region Private Methods

        private void ConfigureBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Styles/Bootstrap/css").Include(
                "~/Styles/Bootstrap/bootstrap.*",
                "~/Styles/Kendo/kendo.common-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Styles/fontawesome/css").Include("~/Styles/FontAwesome/font*"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Styles/css").Include("~/Areas/Admin/Styles/admin*"));

            bundles.Add(new StyleBundle("~/Styles/css").Include("~/Styles/site*"));

            var cultureFiles = new List<string>();

            foreach (var culture in _environment.Cultures.Cultures.Select(c => c.Code))
            {
                var fileName = string.Format("~/Scripts/Kendo/Localization/kendo.culture.{0}.min.js", culture);

                if (System.IO.File.Exists(_environment.MapPath(fileName)))
                {
                    cultureFiles.Add(fileName);
                }
            }

            bundles.Add(new ScriptBundle("~/bundles/framework").Include(
                "~/Scripts/JQuery/jquery*",
                "~/Scripts/Angular/angular*",
                "~/Scripts/Angular/Services/angular*",
                "~/Scripts/Kendo/kendo.core.*",
                "~/Scripts/Kendo/kendo.*").Include(cultureFiles.ToArray()).Include(
                "~/Scripts/Kendo/Widgets/kendo.*",
                "~/Scripts/Utilities/stacktrace*",
                "~/Scripts/Utilities/script*",
                "~/Scripts/StrixIT/strixit.*",
                "~/Scripts/StrixIT/Modules/strixit*",
                "~/Scripts/StrixIT/Controllers/strixit*",
                "~/Scripts/StrixIT/Filters/strixit*",
                "~/Scripts/StrixIT/Services/strixit*").IncludeDirectory("~/Scripts/StrixIT/Directives", "*.js", true).Include(
                "~/Areas/Admin/Scripts/strixit.*"));
        }

        private void ConfigureFilters()
        {
            GlobalFilters.Filters.Add(new LogErrorAttribute());
            GlobalFilters.Filters.Add(new JsonHandlerAttribute());
            GlobalFilters.Filters.Add(new JsonValidateAntiForgeryTokenAttribute());
            GlobalFilters.Filters.Add(new LinkAuthenticationToSessionAttribute());
        }

        private void ConfigureViewEngines()
        {
            ViewEngines.Engines.Clear();
            var razorEngine = new RazorViewEngine();
            ViewEngines.Engines.Add(razorEngine);

            List<string> viewLocations = new List<string>();
            List<string> partialViewLocations = new List<string>();

            foreach (string area in AdminRouteConstraint.AreaNames)
            {
                viewLocations.Add(string.Format("~/Areas/{0}/Views/Shared/{{0}}.cshtml", area));
                viewLocations.Add(string.Format("~/Areas/{0}/Views/{{1}}/{{0}}.cshtml", area));
                partialViewLocations.Add(string.Format("~/Areas/{0}/Views/Shared/{{0}}.cshtml", area));
            }

            razorEngine.ViewLocationFormats = razorEngine.ViewLocationFormats.Concat(viewLocations).ToArray();
            razorEngine.PartialViewLocationFormats = razorEngine.PartialViewLocationFormats.Concat(partialViewLocations).ToArray();
            razorEngine.AreaPartialViewLocationFormats = razorEngine.AreaPartialViewLocationFormats.Concat(partialViewLocations).ToArray();
        }

        #endregion Private Methods
    }
}