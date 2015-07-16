//-----------------------------------------------------------------------
// <copyright file="WebEnvironment.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using StrixIT.Platform.Core;
using Newtonsoft.Json;

namespace StrixIT.Platform.Web
{
    public class WebEnvironment : IEnvironment
    {
        private HttpContextBase _httpContext;

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

                // If the HttpRuntime.AppDomainAppVirtualPath is null, we're not running in a web context although the web
                // environment is loaded. This is the case when running code first migration scripts. Use the default
                // environment working directory in that case.
                return new DefaultEnvironment().WorkingDirectory;
            }
        }

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

        public void StoreInSession(string key, object theObject)
        {
            var context = this.HttpContext;

            if (context != null && context.Session != null)
            {
                context.Session[key.ToLower()] = theObject;
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

        public void AbandonSession()
        {
            var context = this.HttpContext;

            if (context != null && context.Session != null)
            {
                context.Session.Abandon();
            }
        }
    }
}