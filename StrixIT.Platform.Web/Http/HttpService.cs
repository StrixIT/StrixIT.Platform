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

#endregion Apache License

using StrixIT.Platform.Core;
using StrixIT.Platform.Core.Environment;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace StrixIT.Platform.Web
{
    public class HttpService : IHttpService
    {
        #region Private Fields

        private ICultureService _cultureService;
        private HttpContextBase _httpContext;
        private ISessionService _sessionService;

        #endregion Private Fields

        #region Public Constructors

        public HttpService(ICultureService cultureService, ISessionService sessionService, HttpContextBase context)
        {
            _cultureService = cultureService;
            _sessionService = sessionService;
            _httpContext = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public void CompressRequest()
        {
            if (!_httpContext.IsDebuggingEnabled && !Regex.Match(_httpContext.Request.Url.ToString(), WebConstants.RESOURCEREGEX).Success)
            {
                _httpContext.Response.Filter = new GZipStream(_httpContext.Response.Filter, CompressionMode.Compress);
                _httpContext.Response.AppendHeader("Content-encoding", "gzip");
                _httpContext.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
            }
        }

        public void SetCultureForRequest()
        {
            var url = _httpContext.Request.Url.ToString();
            string currentCulture = _cultureService.CurrentCultureCode;

            var culturePattern = string.Format("/{0}/", WebConstants.CULTUREREGEX);
            var match = Regex.Match(_httpContext.Request.AppRelativeCurrentExecutionFilePath, culturePattern);

            if (!match.Success && _httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2).IndexOf("/") <= 0)
            {
                culturePattern = string.Format("/{0}", WebConstants.CULTUREREGEX);
                match = Regex.Match(_httpContext.Request.AppRelativeCurrentExecutionFilePath, culturePattern);
            }

            if (match.Success)
            {
                var newCultureCode = match.Value.Replace("/", string.Empty).ToLower();

                if (newCultureCode != currentCulture)
                {
                    var newCulture = _cultureService.Cultures.Where(cu => cu.Code.ToLower() == newCultureCode).Select(cu => cu.Code).FirstOrDefault();

                    if (newCulture != null)
                    {
                        _sessionService.Store(PlatformConstants.CURRENTCULTURE, newCulture);
                    }
                }
            }
            else
            {
                var defaultCode = _cultureService.DefaultCultureCode;

                if (currentCulture != defaultCode)
                {
                    _sessionService.Store(PlatformConstants.CURRENTCULTURE, defaultCode);
                }
            }
        }

        public void SetResponseHeaders()
        {
            var response = _httpContext.Response;
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

        #endregion Public Methods
    }
}