//-----------------------------------------------------------------------
// <copyright file="AdminRouteConstraint.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Linq;
using System.Web;
using System.Web.Routing;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class AdminRouteConstraint : IRouteConstraint
    {
        private static string[] _cultureCodes;
        private static string[] _areaNames;

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (_cultureCodes == null)
            {
                _cultureCodes = StrixPlatform.Cultures.Select(c => c.Code).ToLower().ToArray();
            }

            if (_areaNames == null)
            {
                _areaNames = Web.Helpers.AreaNames.Where(a => a != WebConstants.ADMIN).ToArray();
            }

            var match = _cultureCodes.Contains(((string)values[PlatformConstants.LANGUAGE]).ToLower());

            if (match)
            {
                var isAreaRoute = values.ContainsKey("area") || values[parameterName].ToString().ToLower() == "admin";
                var isAdminUrl = httpContext.Request.Url != null && httpContext.Request.Url.ToString().ToLower().Contains("/admin");
                var isAreaUrl = !_areaNames.Any(a => a == values[parameterName].ToString().ToLower());

                return isAreaRoute && isAdminUrl && isAreaUrl;
            }

            return false;
        }
    }
}
