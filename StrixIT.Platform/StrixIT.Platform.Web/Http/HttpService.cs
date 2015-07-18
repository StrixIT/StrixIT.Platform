#region Apache License
//-----------------------------------------------------------------------
// <copyright file="HttpService.cs" company="StrixIT">
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

using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class HttpService : IHttpService
    {
        private HttpContextBase _httpContext;

        public HttpService(HttpContextBase context)
        {
            this._httpContext = context;
        }

        public void CompressRequest()
        {
            if (!this._httpContext.IsDebuggingEnabled && !Regex.Match(this._httpContext.Request.Url.ToString(), WebConstants.RESOURCEREGEX).Success)
            {
                this._httpContext.Response.Filter = new GZipStream(this._httpContext.Response.Filter, CompressionMode.Compress);
                this._httpContext.Response.AppendHeader("Content-encoding", "gzip");
                this._httpContext.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
            }
        }

        public void SetResponseHeaders()
        {
            var response = this._httpContext.Response;
            response.Headers.Remove("Server");
            response.Headers.Remove("X-AspNetMvc-Version");
            response.Headers.Remove("X-AspNet-Version");

            if (response.Headers[WebConstants.IFRAMEMODEHEADER] == null)
            {
                if (response.ContentType.ToLower() == "text/html")
                {
                    response.Headers.Add(WebConstants.IFRAMEMODEHEADER, "SAMEORIGIN");
                }
                else
                {
                    response.Headers.Add(WebConstants.IFRAMEMODEHEADER, "DENY");
                }
            }
        }

        public void SetCultureForRequest()
        {
            var url = this._httpContext.Request.Url.ToString();
            string currentCulture = StrixPlatform.CurrentCultureCode;

            var culturePattern = string.Format("/{0}/", WebConstants.CULTUREREGEX);
            var match = Regex.Match(this._httpContext.Request.AppRelativeCurrentExecutionFilePath, culturePattern);

            if (!match.Success && this._httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2).IndexOf("/") <= 0)
            {
                culturePattern = string.Format("/{0}", WebConstants.CULTUREREGEX);
                match = Regex.Match(this._httpContext.Request.AppRelativeCurrentExecutionFilePath, culturePattern);
            }

            if (match.Success)
            {
                var newCultureCode = match.Value.Replace("/", string.Empty).ToLower();

                if (newCultureCode != currentCulture)
                {
                    var newCulture = StrixPlatform.Cultures.Where(cu => cu.Code.ToLower() == newCultureCode).Select(cu => cu.Code).FirstOrDefault();

                    if (newCulture != null)
                    {
                        StrixPlatform.Environment.StoreInSession(PlatformConstants.CURRENTCULTURE, newCulture);
                    }
                }
            }
            else
            {
                var defaultCode = StrixPlatform.DefaultCultureCode;

                if (currentCulture != defaultCode)
                {
                    StrixPlatform.Environment.StoreInSession(PlatformConstants.CURRENTCULTURE, defaultCode);
                }
            }
        }
    }
}