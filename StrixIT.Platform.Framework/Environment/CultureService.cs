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

using StrixIT.Platform.Core;
using StrixIT.Platform.Core.Environment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StrixIT.Platform.Framework.Environment
{
    public class CultureService : ICultureService
    {
        #region Private Fields

        private static IList<CultureData> _cultures;
        private static string _currentCulture;
        private static string _defaultCultureCode;
        private IConfiguration _config;
        private ISessionService _session;

        #endregion Private Fields

        #region Public Constructors

        public CultureService(IConfiguration config, ISessionService session)
        {
            _config = config;
            _session = session;
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<CultureData> Cultures
        {
            get
            {
                if (_cultures == null)
                {
                    var list = new List<CultureData>();
                    var codes = _config.GetConfiguration<PlatformConfiguration>().Cultures;

                    foreach (var code in codes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Trim())
                    {
                        var culture = CultureInfo.GetCultureInfo(code);
                        list.Add(new CultureData { Code = code, Name = culture.Name, NativeName = culture.NativeName });
                    }

                    _cultures = list;
                }

                return _cultures;
            }
        }

        public string CurrentCultureCode
        {
            get
            {
                if (_currentCulture != null)
                {
                    return _currentCulture;
                }

                var culture = _session.Get<string>(PlatformConstants.CURRENTCULTURE);

                if (string.IsNullOrWhiteSpace(culture))
                {
                    culture = DefaultCultureCode;
                }

                return culture;
            }
            set
            {
                _currentCulture = value;
            }
        }

        public string DefaultCultureCode
        {
            get
            {
                if (_defaultCultureCode == null)
                {
                    var list = Cultures;
                    _defaultCultureCode = list.Count > 0 ? list.First().Code : null;
                }

                return _defaultCultureCode;
            }
        }

        #endregion Public Properties
    }
}