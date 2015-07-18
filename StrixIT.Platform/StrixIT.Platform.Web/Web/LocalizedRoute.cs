#region Apache License
//-----------------------------------------------------------------------
// <copyright file="LocalizedRoute.cs" company="StrixIT">
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
#endregion

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// This route is used to work with cultures.
    /// </summary>
    public class LocalizedRoute : Route
    {
        private static string _culture = StrixPlatform.DefaultCultureCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedRoute" /> class, by using
        /// the specified URL pattern, default parameter values, constraints, custom
        /// values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public LocalizedRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedRoute" /> class, by using
        /// the specified URL pattern, default parameter values, constraints, custom
        /// values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedRoute" /> class, by using
        /// the specified URL pattern, default parameter values, constraints, custom
        /// values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param> 
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedRoute" /> class, by using
        /// the specified URL pattern, default parameter values, constraints, custom
        /// values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine 
        /// whether the route matches a specific URL pattern. These values are passed to the route handler, where they can 
        /// be used for processing the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }

        #region GetRouteData

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isResource = Regex.Match(httpContext.Request.Url.ToString(), @"\.\w{2,4}$").Success;

            if (isResource)
            {
                return base.GetRouteData(httpContext);
            }

            string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;

            var parts = virtualPath.Substring(2).Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            Guid parseResult;

            if (parts.Length == 0
                || parts[0].ToLower() == "bundles"
                || parts[0].ToLower() == "styles"
                || parts[0].ToLower() == "content"
                || parts[0].ToLower() == "areas"
                || Guid.TryParse(parts[0].ToLower(), out parseResult))
            {
                return base.GetRouteData(httpContext);
            }

            var urlParts = this.Url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            bool correctArea = parts.Count() >= 1 ? !urlParts[0].StartsWith("{") ? urlParts[0].ToLower() == parts[0].ToLower() : true : true;

            if (correctArea)
            {
                bool isArea = parts.Count() >= 1 ? urlParts[0].ToLower() == parts[0].ToLower() : false;
                var culturePattern = "/[a-zA-Z]{2}$|/[a-zA-Z]{2}/|/[a-zA-Z]{2}-[a-zA-Z]{2}/";
                var match = Regex.Match(virtualPath.Substring(1), culturePattern);

                if (!match.Success)
                {
                    var newPath = virtualPath.Substring(2).ToLower();

                    if (isArea)
                    {
                        newPath = newPath.IndexOf("/") > -1 ? newPath.Insert(newPath.IndexOf("/") + 1, _culture + "/") : newPath + "/" + _culture;
                    }
                    else
                    {
                        newPath = _culture + "/" + newPath;
                    }

                    newPath = "~/" + newPath;
                    httpContext.RewritePath(newPath, true);
                }
            }

            return base.GetRouteData(httpContext);
        }

        #endregion

        #region GetVirtualPath

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>
        /// An object that contains information about the URL that is associated with the route.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var result = base.GetVirtualPath(requestContext, values);

            if (result != null)
            {
                var language = values[PlatformConstants.LANGUAGE] != null ? values[PlatformConstants.LANGUAGE] : requestContext.RouteData.Values[PlatformConstants.LANGUAGE];
                result.VirtualPath = result.VirtualPath.ToLower();
                string startPattern = _culture + "/";
                string containsPattern = "/" + _culture + "/";

                if (language != null && result.VirtualPath.StartsWith(startPattern))
                {
                    result.VirtualPath = result.VirtualPath.Replace(startPattern, string.Empty);
                }
                else if (language != null && result.VirtualPath.Contains(containsPattern))
                {
                    result.VirtualPath = result.VirtualPath.Replace(containsPattern, "/");
                }
            }

            return result;
        }

        #endregion
    }
}