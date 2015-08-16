#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A logger singleton to allow for easy logging.
    /// </summary>
    public static class Logger
    {
        #region Private Fields

        private static ILoggingService _loggingService;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the logging service to use.
        /// </summary>
        public static ILoggingService LoggingService
        {
            get
            {
                if (_loggingService == null && DependencyInjector.Injector != null)
                {
                    _loggingService = DependencyInjector.TryGet<ILoggingService>();
                }

                return _loggingService;
            }
            set
            {
                _loggingService = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Logs the specified message to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">The level</param>
        public static void Log(string message, LogLevel level = LogLevel.Debug)
        {
            var service = LoggingService;

            if (service != null)
            {
                service.Log(message, level);
            }
        }

        /// <summary>
        /// Logs the specified message and exception to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="level">The level</param>
        public static void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            var service = LoggingService;

            if (service != null)
            {
                service.Log(message, exception, level);
            }
        }

        /// <summary>
        /// Logs analytics data.
        /// </summary>
        /// <param name="entryType">The data type</param>
        /// <param name="data">The data</param>
        public static void LogToAnalytics(string entryType, string data)
        {
            var service = LoggingService;

            if (service != null)
            {
                service.LogToAnalytics(entryType, data);
            }
        }

        /// <summary>
        /// Logs an audit message.
        /// </summary>
        /// <param name="entryType">The message type</param>
        /// <param name="message">The message</param>
        public static void LogToAudit(string entryType, string message)
        {
            var service = LoggingService;

            if (service != null)
            {
                service.LogToAudit(entryType, message);
            }
        }

        #endregion Public Methods
    }
}