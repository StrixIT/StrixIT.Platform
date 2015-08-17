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

        private HttpSessionStateBase _httpSession;

        #endregion Private Fields

        #region Public Constructors

        public SessionService(HttpSessionStateBase httpSession)
        {
            _httpSession = httpSession;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Abandon()
        {
            if (_httpSession != null)
            {
                _httpSession.Abandon();
            }
        }

        public T Get<T>(string key)
        {
            if (_httpSession == null)
            {
                return default(T);
            }

            var result = _httpSession[key.ToLower()];

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

            if (_httpSession != null)
            {
                var valuesToExclude = new string[] { PlatformConstants.CURRENTUSER.ToLower(), PlatformConstants.CURRENTUSEREMAIL.ToLower(), PlatformConstants.CURRENTUSERGROUPS.ToLower() };

                foreach (var key in _httpSession.Keys)
                {
                    if (!valuesToExclude.Contains(key.ToString().ToLower()))
                    {
                        var value = _httpSession[(string)key];
                        dictionary.Add((string)key, JsonConvert.SerializeObject(value));
                    }
                }

                return dictionary;
            }

            return dictionary;
        }

        public void Store(string key, object value)
        {
            if (_httpSession != null)
            {
                _httpSession[key.ToLower()] = value;
            }
        }

        #endregion Public Methods
    }
}