#region Apache License

//-----------------------------------------------------------------------
// <copyright file="StrixPlatform.cs" company="StrixIT">
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to offer access to general data for the platform.
    /// </summary>
    public static class StrixPlatform
    {
        #region Private Fields

        private static Guid _applicationId;
        private static IList<CultureData> _cultures;
        private static string _currentCulture;
        private static string _defaultCultureCode;
        private static IEnvironment _environment;
        private static Guid _mainGroupId;
        private static IList<string> _startupMessages = new List<string>();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the application id.
        /// </summary>
        public static Guid ApplicationId
        {
            get
            {
                if (_applicationId == Guid.Empty)
                {
                    var membershipService = DependencyInjector.TryGet<IMembershipService>();
                    _applicationId = membershipService != null ? membershipService.ApplicationId : Guid.Empty;
                }

                return _applicationId;
            }
            set
            {
                _applicationId = value;
            }
        }

        /// <summary>
        /// Gets the platform configuration.
        /// </summary>
        public static PlatformConfigurationSection Configuration
        {
            get
            {
                return ConfigurationManager.GetSection("strixPlatform") as PlatformConfigurationSection;
            }
        }

        /// <summary>
        /// Gets all available culture codes, names and native names for the application.
        /// </summary>
        /// <returns>The culture data</returns>
        public static IList<CultureData> Cultures
        {
            get
            {
                if (_cultures == null)
                {
                    var list = new List<CultureData>();
                    var codes = Configuration.Cultures;

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

        /// <summary>
        /// Gets or sets the current culture code for the application.
        /// </summary>
        public static string CurrentCultureCode
        {
            get
            {
                if (_currentCulture != null)
                {
                    return _currentCulture;
                }

                var culture = Environment.GetFromSession<string>(PlatformConstants.CURRENTCULTURE);

                if (string.IsNullOrWhiteSpace(culture))
                {
                    culture = StrixPlatform.DefaultCultureCode;
                }

                return culture;
            }
            set
            {
                _currentCulture = value;
            }
        }

        /// <summary>
        /// Gets the default culture code for the application.
        /// </summary>
        public static string DefaultCultureCode
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

        /// <summary>
        /// Gets or sets the current environment.
        /// </summary>
        public static IEnvironment Environment
        {
            get
            {
                if (_environment == null)
                {
                    _environment = Helpers.GetImplementationOrDefault<IEnvironment, DefaultEnvironment>();
                }

                return _environment;
            }
            set
            {
                _environment = value;
            }
        }

        /// <summary>
        /// Gets or sets the main group id.
        /// </summary>
        public static Guid MainGroupId
        {
            get
            {
                if (_mainGroupId == Guid.Empty)
                {
                    var membershipService = DependencyInjector.TryGet<IMembershipService>();
                    _mainGroupId = membershipService != null ? membershipService.MainGroupId : Guid.Empty;
                }

                return _mainGroupId;
            }
            set
            {
                _mainGroupId = value;
            }
        }

        /// <summary>
        /// Gets the startup messages logged when the platform starts.
        /// </summary>
        public static IList<string> StartupMessages
        {
            get
            {
                return _startupMessages;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Raises an event for the platform.
        /// </summary>
        /// <typeparam name="T">The type of the event to raise</typeparam>
        /// <param name="args">The event to raise</param>
        public static void RaiseEvent<T>(T args) where T : IPlatformEvent
        {
            foreach (var handler in DependencyInjector.GetAll<IHandlePlatformEvent<T>>())
            {
                handler.Handle(args);
            }
        }

        /// <summary>
        /// Logs a startup message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message</param>
        public static void WriteStartupMessage(string message, LogLevel level = LogLevel.Debug)
        {
            string fullPattern = DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            fullPattern = Regex.Replace(fullPattern, "(:ss|:s)", "$1.fff");
            message = string.Format("At: {0}. Level: {1}. Message: {2}", DateTime.Now.ToString(fullPattern), level, message);
            _startupMessages.Add(message);
        }

        #endregion Public Methods
    }
}