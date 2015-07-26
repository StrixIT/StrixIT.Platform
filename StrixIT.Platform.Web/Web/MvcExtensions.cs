#region Apache License

//-----------------------------------------------------------------------
// <copyright file="MvcExtensions.cs" company="StrixIT">
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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StrixIT.Platform.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace StrixIT.Platform.Web
{
    public static class MvcExtensions
    {
        private const string DISPLAY = "display";

        private static List<string> Scripts
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(WebConstants.PARTIALSCRIPTS))
                {
                    HttpContext.Current.Items[WebConstants.PARTIALSCRIPTS] = new List<string>();
                }

                return HttpContext.Current.Items[WebConstants.PARTIALSCRIPTS] as List<string>;
            }
        }

        private static List<string> Styles
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(WebConstants.PARTIALSTYLES))
                {
                    HttpContext.Current.Items[WebConstants.PARTIALSTYLES] = new List<string>();
                }

                return HttpContext.Current.Items[WebConstants.PARTIALSTYLES] as List<string>;
            }
        }

        /// <summary>
        /// Creates a data records wrapper for an enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable</param>
        /// <param name="options">The filter options used to get the data</param>
        /// <returns>The data records wrapper</returns>
        public static DataRecords DataRecords(this IEnumerable enumerable, FilterOptions options)
        {
            return new DataRecords(enumerable, options);
        }

        /// <summary>
        /// Serialize an object to Json.
        /// </summary>
        /// <param name="value">The object to serialze</param>
        /// <returns>The Json string</returns>
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        #region Localized Route

        /// <summary>
        /// Maps the specified URL route and sets default route values.
        /// </summary>
        /// <returns>A reference to the mapped route</returns>
        /// <param name="routes">A collection of routes for the application.</param><param name="name">The name of the route to map.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><exception cref="T:System.ArgumentNullException">The <paramref name="routes"/> or <paramref name="url"/> parameter is null.</exception>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapLocalizedRoute(routes, name, url, defaults, (object)null);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values and constraints.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="routes">A collection of routes for the application.</param><param name="name">The name of the route to map.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="constraints">A set of expressions that specify values for the <paramref name="url"/> parameter.</param><exception cref="T:System.ArgumentNullException">The <paramref name="routes"/> or <paramref name="url"/> parameter is null.</exception>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return MapLocalizedRoute(routes, name, url, defaults, constraints, (string[])null);
        }

        /// <summary>
        /// Maps the specified URL route and sets the namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="routes">A collection of routes for the application.</param><param name="name">The name of the route to map.</param><param name="url">The URL pattern for the route.</param><param name="namespaces">A set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="routes"/> or <paramref name="url"/> parameter is null.</exception>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return MapLocalizedRoute(routes, name, url, (object)null, (object)null, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="routes">A collection of routes for the application.</param><param name="name">The name of the route to map.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="namespaces">A set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="routes"/> or <paramref name="url"/> parameter is null.</exception>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapLocalizedRoute(routes, name, url, defaults, (object)null, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values, constraints, and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="routes">A collection of routes for the application.</param><param name="name">The name of the route to map.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="constraints">A set of expressions that specify values for the <paramref name="url"/> parameter.</param><param name="namespaces">A set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="routes"/> or <paramref name="url"/> parameter is null.</exception>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (constraints == null)
            {
                var culturePattern = "[a-zA-Z]{2}|[a-zA-Z]{2}-[a-zA-Z]{2}";
                constraints = new { language = culturePattern };
            }

            Route route1 = new LocalizedRoute(url, (IRouteHandler)new MvcRouteHandler());
            route1.Defaults = new RouteValueDictionary(defaults);
            route1.Constraints = new RouteValueDictionary(constraints);
            route1.DataTokens = new RouteValueDictionary();
            Route route2 = route1;

            if (namespaces != null && namespaces.Length > 0)
            {
                route2.DataTokens["Namespaces"] = (object)namespaces;
            }

            routes.Add(name, (RouteBase)route2);
            return route2;
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url)
        {
            return MapLocalizedRoute(context, name, url, (object)null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            return MapLocalizedRoute(context, name, url, defaults, (object)null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values and constraint.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify valid values for a URL parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints)
        {
            return MapLocalizedRoute(context, name, url, defaults, constraints, (string[])null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="namespaces">An enumerable set of namespaces for the application.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url, string[] namespaces)
        {
            return MapLocalizedRoute(context, name, url, (object)null, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="namespaces">An enumerable set of namespaces for the application.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
        {
            return MapLocalizedRoute(context, name, url, defaults, (object)null, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values, constraints, and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="context">The registration context to use</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify valid values for a URL parameter.</param>
        /// <param name="namespaces">An enumerable set of namespaces for the application.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        /// <returns>The route</returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (namespaces == null && context.Namespaces != null)
            {
                namespaces = Enumerable.ToArray<string>((IEnumerable<string>)context.Namespaces);
            }

            if (constraints == null)
            {
                var culturePattern = "[a-zA-Z]{2}|[a-zA-Z]{2}-[a-zA-Z]{2}";
                constraints = new { language = culturePattern };
            }

            Route route = MapLocalizedRoute(context.Routes, name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = (object)context.AreaName;
            bool flag = namespaces == null || namespaces.Length == 0;
            route.DataTokens["UseNamespaceFallback"] = (object)(bool)(flag ? true : false);
            return route;
        }

        #endregion Localized Route

        #region Admin Links

        /// <summary>
        /// Create a link for use on the platform administration interface
        /// </summary>
        /// <param name="helper">The helper to render the link</param>
        /// <param name="action">The action to create the link for</param>
        /// <returns>The admin interface link</returns>
        public static string AdminLink(this UrlHelper helper, string action)
        {
            return AdminLink(helper, action, null, null);
        }

        /// <summary>
        /// Create a link for use on the platform administration interface
        /// </summary>
        /// <param name="helper">The helper to render the link</param>
        /// <param name="action">The action to create the link for</param>
        /// <param name="controller">The controller to create the link for</param>
        /// <returns>The admin interface link</returns>
        public static string AdminLink(this UrlHelper helper, string action, string controller)
        {
            return AdminLink(helper, action, controller, null);
        }

        /// <summary>
        /// Create a link for use on the platform administration interface
        /// </summary>
        /// <param name="helper">The helper to render the link</param>
        /// <param name="action">The action to create the link for</param>
        /// <param name="controller">The controller to create the link for</param>
        /// <param name="module">The module to create the link for</param>
        /// <returns>The admin interface link</returns>
        public static string AdminLink(this UrlHelper helper, string action, string controller, string module)
        {
            string link;

            if (!string.IsNullOrWhiteSpace(module))
            {
                link = helper.Action(action, controller, new { area = module });
            }
            else
            {
                link = helper.Action(action, controller);
            }

            link = link.ToLower().Replace(MvcConstants.INDEX.ToLower(), string.Empty);
            var root = helper.Content("~");
            var rootIndex = link.IndexOf(root) + root.Length;
            var language = (string)helper.RequestContext.RouteData.Values[PlatformConstants.LANGUAGE];

            if (language != null && language != StrixPlatform.DefaultCultureCode.ToLower())
            {
                rootIndex += language.Length + 1;
            }

            link = link.Replace(string.Format("/{0}/", StrixPlatform.DefaultCultureCode.ToLower()), "/");

            string admin = string.Empty;

            if (!link.Contains(string.Format("/{0}/", WebConstants.ADMIN)))
            {
                admin = string.Format("{0}/", WebConstants.ADMIN);
            }

            link = string.Format("{0}{1}{2}", link.Substring(0, rootIndex), admin, link.Substring(rootIndex));
            link = link.Replace("//", "/");

            if (link.EndsWith("/"))
            {
                link = link.Substring(0, link.Length - 1);
            }

            return link;
        }

        #endregion Admin Links

        #region Render Razor templates

        /// <summary>
        /// Render a razor view to a string.
        /// </summary>
        /// <param name="context">The controller context to render the view</param>
        /// <param name="viewPath">The view path</param>
        /// <param name="model">The model to use for rendering the view</param>
        /// <param name="partial">True if the view to render is a partial, false otherwise</param>
        /// <returns>A string containing the rendered HTML</returns>
        public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
            {
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            }
            else
            {
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
            }

            if (viewEngineResult == null)
            {
                throw new FileNotFoundException("View cannot be found.");
            }

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(
                    context,
                    view,
                    context.Controller.ViewData,
                    context.Controller.TempData,
                    sw);

                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        #endregion Render Razor templates

        // Code taken from http://blog.falafel.com/loading-javascript-css-resources-nested-asp-net-partial-views/. Adapted to work for partials only.

        #region Scrips and Styles for Partials

        /// <summary>
        /// Renders the scripts onto the page that were added by the view and partial views.
        /// </summary>
        /// <param name="html">The HTMLHelper</param>
        /// <returns>The rendered script tag</returns>
        public static MvcHtmlString RenderPartialScripts(this HtmlHelper html)
        {
            var sb = new StringBuilder();

            foreach (var script in Scripts)
            {
                sb.AppendLine(string.Format("<script src=\"{0}\"></script>", UrlHelper.GenerateContentUrl(script, html.ViewContext.HttpContext)));
            }
            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Renders the stylesheets onto the page that were added by the view and partial views.
        /// </summary>
        /// <param name="html">The HTMLHelper</param>
        /// <returns>The rendered style tag</returns>
        public static MvcHtmlString RenderPartialStyleSheets(this HtmlHelper html)
        {
            var sb = new StringBuilder();

            foreach (var style in Styles)
            {
                sb.AppendLine(string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\">", UrlHelper.GenerateContentUrl(style, html.ViewContext.HttpContext)));
            }
            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Adds a script to be emitted by the RenderScripts method.
        /// </summary>
        /// <param name="html">The HTMLHelper.</param>
        /// <param name="scriptUrl">The script URL.</param>
        public static void AddScript(this HtmlHelper html, string scriptUrl)
        {
            Scripts.Add(scriptUrl);
        }

        /// <summary>
        /// Adds a stylesheet to be emitted by the RenderStyleSheets method.
        /// </summary>
        /// <param name="html">The HTMLHelper</param>
        /// <param name="styleUrl">The stylesheet URL.</param>
        public static void AddStyleSheet(this HtmlHelper html, string styleUrl)
        {
            Styles.Add(styleUrl);
        }

        #endregion Scrips and Styles for Partials

        #region Content Links

        /// <summary>
        /// Display module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="controllerName">The content controller name</param>
        /// <param name="url">The content url</param>
        /// <returns>The content</returns>
        public static IHtmlString Content(this HtmlHelper helper, string controllerName, string url)
        {
            return Content(helper, new DisplayOptions(controllerName, url) { Action = "display" });
        }

        /// <summary>
        /// Display module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="moduleName">The content controller name</param>
        /// <param name="controllerName">The content action name</param>
        /// <param name="url">The content url</param>
        /// <returns>The content</returns>
        public static IHtmlString Content(this HtmlHelper helper, string moduleName, string controllerName, string url)
        {
            return Content(helper, new DisplayOptions(moduleName, controllerName, "display", url, null, null));
        }

        /// <summary>
        /// Display module content that has child items on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="controllerName">The content controller name</param>
        /// <param name="url">The content url</param>
        /// <returns>The content</returns>
        public static IHtmlString ContentWithChild(this HtmlHelper helper, string controllerName, string url)
        {
            return Content(helper, new DisplayOptions(controllerName, url) { HasChildPages = true });
        }

        /// <summary>
        /// Display module content that has child items on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="moduleName">The content module name</param>
        /// <param name="controllerName">The content controller name</param>
        /// <param name="url">The content url</param>
        /// <returns>The content</returns>
        public static IHtmlString ContentWithChild(this HtmlHelper helper, string moduleName, string controllerName, string url)
        {
            return Content(helper, new DisplayOptions(moduleName, controllerName, null, url, null, null));
        }

        /// <summary>
        /// Display a list of module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="controllerName">The content controller name</param>
        /// <returns>The content list</returns>
        public static IHtmlString ContentList(this HtmlHelper helper, string controllerName)
        {
            return Content(helper, new DisplayOptions(controllerName));
        }

        /// <summary>
        /// Display a list of module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="moduleName">The content module name</param>
        /// <param name="controllerName">The content controller name</param>
        /// <returns>The content list</returns>
        public static IHtmlString ContentList(this HtmlHelper helper, string moduleName, string controllerName)
        {
            return Content(helper, new DisplayOptions(moduleName, controllerName, null, null, null, null));
        }

        /// <summary>
        /// Display a list of module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="moduleName">The content module name</param>
        /// <param name="controllerName">The content controller name</param>
        /// <param name="itemPageUrl">The url of the page on which to show the individual content items</param>
        /// <returns>The content list</returns>
        public static IHtmlString ContentList(this HtmlHelper helper, string moduleName, string controllerName, string itemPageUrl)
        {
            return Content(helper, new DisplayOptions(moduleName, controllerName, null, null, itemPageUrl, null));
        }

        /// <summary>
        /// Display module content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="options">The content display options</param>
        /// <returns>The content</returns>
        public static IHtmlString Content(this HtmlHelper helper, DisplayOptions options)
        {
            var controller = GetController(helper, options.Controller, options.Action);

            if (controller != null)
            {
                var getContentEvent = new GetContentEvent(helper, options);
                StrixPlatform.RaiseEvent(getContentEvent);

                if (getContentEvent.Result != null)
                {
                    return getContentEvent.Result;
                }
            }

            return new HtmlString(options.DefaultText);
        }

        /// <summary>
        /// Displays widget content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="controllerName">The name of the controller of the widget</param>
        /// <param name="action">The name of the action of the widget</param>
        /// <returns>The widget content</returns>
        public static IHtmlString Widget(this HtmlHelper helper, string controllerName, string action)
        {
            return Widget(helper, controllerName, action, null);
        }

        /// <summary>
        /// Displays widget content on a page.
        /// </summary>
        /// <param name="helper">The html helper to render the content</param>
        /// <param name="controllerName">The name of the controller of the widget</param>
        /// <param name="action">The name of the action of the widget</param>
        /// <param name="mainUrl">The url of the main content page, if the content uses child pages</param>
        /// <returns>The widget content</returns>
        public static IHtmlString Widget(this HtmlHelper helper, string controllerName, string action, string mainUrl)
        {
            var path = helper.ViewContext.RequestContext.HttpContext.Request.Path.ToLower();
            path = path.Replace(string.Format("/{0}/", StrixPlatform.CurrentCultureCode), string.Empty).Replace("/index", string.Empty);
            var url = path.Substring(path.LastIndexOf("/") + 1);

            if (!string.IsNullOrWhiteSpace(mainUrl))
            {
                if (path.Any(c => c == '/'))
                {
                    url = string.Format("{0}/{1}", mainUrl, url);
                }
                else
                {
                    url = mainUrl.ToLower();
                }
            }

            return helper.Action(action, controllerName, new { url = url });
        }

        #endregion Content Links

        #region RenderHtml

        public static IHtmlString RenderEscapedHtml(this HtmlHelper helper, string text)
        {
            return helper.Raw(Web.Helpers.HtmlDecode(text));
        }

        public static IHtmlString RenderHtml(this HtmlHelper helper, string text)
        {
            return helper.Raw(Web.Helpers.HtmlDecode(text, false));
        }

        #endregion RenderHtml

        #region Private Methods

        private static IController GetController(HtmlHelper helper, string controllerName, string actionName)
        {
            IController controller = null;

            try
            {
                var factory = ControllerBuilder.Current.GetControllerFactory();
                controller = factory.CreateController(helper.ViewContext.RequestContext, controllerName);

                if (controller != null)
                {
                    var methods = controller.GetType().GetMethods();

                    if (!methods.Any(m => m.Name.ToLower() == actionName.ToLower()))
                    {
                        controller = null;
                    }
                }
            }
            catch (HttpException)
            {
                controller = null;
            }

            return controller;
        }

        #endregion Private Methods
    }
}