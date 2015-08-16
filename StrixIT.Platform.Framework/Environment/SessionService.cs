#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DefaultEnvironment.cs" company="StrixIT">
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
using StrixIT.Platform.Core.Environment;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrixIT.Platform.Framework.Environment
{
    public class SessionService : ISessionService
    {
        #region Private Fields

        private HttpContextBase _httpContext;

        #endregion Private Fields

        #region Public Constructors

        public SessionService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Abandon()
        {
            if (_httpContext != null && _httpContext.Session != null)
            {
                _httpContext.Session.Abandon();
            }
        }

        public T Get<T>(string key)
        {
            if (_httpContext == null || _httpContext.Session == null)
            {
                return default(T);
            }

            var result = _httpContext.Session[key.ToLower()];

            if (result == null)
            {
                return default(T);
            }

            var returnType = typeof(T);
            var resultType = result.GetType();

            if (resultType.Equals(typeof(string)) && (!returnType.Equals(typeof(string)) || ((string)result).Contains("\"")))
            {
                var deserialized = JsonConvert.DeserializeObject<T>((string)result);
                Store(key, deserialized);
                return deserialized;
            }
            else
            {
                return (T)result;
            }
        }

        public IDictionary<string, object> GetAll()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();

            if (_httpContext != null && _httpContext.Session != null)
            {
                dictionary = GetSessionDictionary(_httpContext.Session);
            }

            return dictionary;
        }

        public void Store(string key, object value)
        {
            if (_httpContext != null && _httpContext.Session != null)
            {
                _httpContext.Session[key.ToLower()] = value;
            }
        }

        #endregion Public Methods

        #region Internal Methods

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

        #endregion Internal Methods
    }
}