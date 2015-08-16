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
                    _areaNames = DependencyInjector.GetTypeList(typeof(AreaRegistration)).Select(a => a.Name.Replace("AreaRegistration", string.Empty).ToLower()).ToArray();
                }

                return _areaNames;
            }
        }

        #endregion Internal Properties

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