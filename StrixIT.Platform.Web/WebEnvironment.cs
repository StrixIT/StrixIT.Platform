#region Apache License

//-----------------------------------------------------------------------
// <copyright file="WebEnvironment.cs" company="StrixIT">
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
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace StrixIT.Platform.Web
{
    public class WebEnvironment : IEnvironment
    {
        #region Private Fields

        private HttpContextBase _httpContext;

        #endregion Private Fields

        #region Public Properties

        public string CurrentUserEmail
        {
            get
            {
                var context = this.HttpContext;

                if (context == null || context.User == null)
                {
                    return null;
                }

                return context.User.Identity.Name;
            }
        }

        public HttpContextBase HttpContext
        {
            get
            {
                if (this._httpContext == null)
                {
                    var current = System.Web.HttpContext.Current;
                    return current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null;
                }

                return this._httpContext;
            }
            set
            {
                this._httpContext = value;
            }
        }

        public string WorkingDirectory
        {
            get
            {
                if (HttpRuntime.AppDomainAppVirtualPath != null)
                {
                    return HttpRuntime.AppDomainAppPath;
                }

                // If the HttpRuntime.AppDomainAppVirtualPath is null, we're not running in a web
                // context although the web environment is loaded. This is the case when running
                // code first migration scripts. Use the default environment working directory in
                // that case.
                return new DefaultEnvironment().WorkingDirectory;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void AbandonSession()
        {
            var context = this.HttpContext;

            if (context != null && context.Session != null)
            {
                context.Session.Abandon();
            }
        }

        public T GetFromSession<T>(string key)
        {
            var context = this.HttpContext;

            if (context == null || context.Session == null)
            {
                return default(T);
            }

            var result = context.Session[key.ToLower()];

            if (result == null)
            {
                return default(T);
            }

            var returnType = typeof(T);
            var resultType = result.GetType();

            if (resultType.Equals(typeof(string)) && (!returnType.Equals(typeof(string)) || ((string)result).Contains("\"")))
            {
                var deserialized = JsonConvert.DeserializeObject<T>((string)result);
                this.StoreInSession(key, deserialized);
                return deserialized;
            }
            else
            {
                return (T)result;
            }
        }

        public IDictionary<string, object> GetSessionDictionary()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            var context = this.HttpContext;

            if (context != null && context.Session != null)
            {
                dictionary = Helpers.GetSessionDictionary(context.Session);
            }

            return dictionary;
        }

        public string MapPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (!Path.IsPathRooted(path))
            {
                path = string.Format("~/{0}", path.Replace("\\", "/"));
            }

            bool isVirtual = !Regex.Match(path, @"[a-zA-Z]:\\[(\w+.\\]{1,}").Success;

            if (isVirtual)
            {
                path = this.HttpContext.Server.MapPath(path);
            }

            return path;
        }

        public void StoreInSession(string key, object theObject)
        {
            var context = this.HttpContext;

            if (context != null && context.Session != null)
            {
                context.Session[key.ToLower()] = theObject;
            }
        }

        #endregion Public Methods
    }
}