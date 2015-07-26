#region Apache License

//-----------------------------------------------------------------------
// <copyright file="AdminRouteConstraint.cs" company="StrixIT">
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
using System.Web;
using System.Web.Routing;

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