#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="StrixIT">
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
using StrixIT.Platform.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public static class Helpers
    {
        #region Private Fields

        private static string[] _areaNames;

        #endregion Private Fields

        #region Internal Properties

        internal static string[] AreaNames
        {
            get
            {
                if (_areaNames == null)
                {
                    _areaNames = ModuleManager.GetTypeList(typeof(AreaRegistration)).Select(a => a.Name.Replace("AreaRegistration", string.Empty).ToLower()).ToArray();
                }

                return _areaNames;
            }
        }

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Gets the virtual path for a physical path.
        /// </summary>
        /// <param name="physicalPath">The physical path to get the virtual path for</param>
        /// <returns>The virtual path</returns>
        public static string GetVirtualPath(string physicalPath)
        {
            string virtualPath = physicalPath;

            if (physicalPath == null)
            {
                throw new ArgumentNullException("physicalPath");
            }

            bool isPhysical = Regex.Match(physicalPath, @"[a-zA-Z]:\\[(\w+.\\]{1,}").Success;

            if (isPhysical)
            {
                var root = StrixPlatform.Environment.WorkingDirectory;
                var pathInRoot = physicalPath.Replace(root, string.Empty);
                virtualPath = pathInRoot.Replace("\\", "/");

                if (virtualPath.StartsWith("/"))
                {
                    virtualPath = virtualPath.Substring(1);
                }
            }

            return virtualPath;
        }

        /// <summary>
        /// HTML decodes a text.
        /// </summary>
        /// <param name="text">The text to HTML decode</param>
        /// <param name="replaceDangerousCharacters">
        /// True if dangerous characters should be replaced by html-safe versions, false if not
        /// </param>
        /// <returns>The HTML decoded text</returns>
        public static string HtmlDecode(string text, bool replaceDangerousCharacters = true)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var decoded = WebUtility.HtmlDecode(text);

            if (replaceDangerousCharacters)
            {
                decoded = decoded.Replace("&", "&amp;")
                                 .Replace("<", "&lt;")
                                 .Replace(">", "&gt;")
                                 .Replace("\"", "&quot;")
                                 .Replace("\'", "&#39;")
                                 .Replace("/", "&#47;");
            }

            return decoded;
        }

        /// <summary>
        /// HTML encodes a text.
        /// </summary>
        /// <param name="text">The text to HTML encode</param>
        /// <returns>The HTML encoded text</returns>
        public static string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }

        #endregion Public Methods

        #region Internal Methods

        internal static bool CustomErrorsEnabled(HttpRequestBase request)
        {
            var systemWeb = Core.Helpers.GetConfigSectionGroup<SystemWebSectionGroup>("system.web");
            return systemWeb.CustomErrors.Mode != CustomErrorsMode.Off && !(systemWeb.CustomErrors.Mode == CustomErrorsMode.RemoteOnly && request.IsLocal);
        }

        internal static IDictionary<string, object> GetSessionDictionary(HttpSessionStateBase session)
        {
            var dictionary = new Dictionary<string, object>();
            var valuesToExclude = new string[] { PlatformConstants.CURRENTUSER.ToLower(), PlatformConstants.CURRENTUSEREMAIL.ToLower(), PlatformConstants.CURRENTUSERGROUPS.ToLower() };

            foreach (var key in session.Keys)
            {
                if (!valuesToExclude.Contains(key.ToString().ToLower()))
                {
                    var value = session[(string)key];
                    dictionary.Add((string)key, JsonConvert.SerializeObject(value));
                }
            }

            return dictionary;
        }

        internal static void Redirect(HttpResponseBase response, string url)
        {
            string redirectUrl;

            if (StrixPlatform.CurrentCultureCode.ToLower() == StrixPlatform.DefaultCultureCode.ToLower())
            {
                redirectUrl = string.Format("~/{0}", url);
            }
            else
            {
                redirectUrl = string.Format("~/{0}/{1}", StrixPlatform.CurrentCultureCode, url);
            }

            response.Redirect(redirectUrl);
        }

        #endregion Internal Methods
    }
}