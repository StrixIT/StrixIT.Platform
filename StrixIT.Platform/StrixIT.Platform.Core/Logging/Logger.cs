//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A logger singleton to allow for easy logging.
    /// </summary>
    public static class Logger
    {
        private static Type _loggingServiceType;
        private static ILoggingService _loggingService;

        /// <summary>
        /// Gets or sets the logging service to use.
        /// </summary>
        public static ILoggingService LoggingService
        {
            get
            {
                if (_loggingService == null)
                {
                    if (_loggingServiceType == null)
                    {
                        _loggingServiceType = Helpers.GetInjectedOrDefaultType<ILoggingService, NullLogger>();
                    }

                    return DependencyInjector.Get(_loggingServiceType) as ILoggingService;
                }

                return _loggingService;
            }
            set
            {
                _loggingService = value;
            }
        }

        /// <summary>
        /// Logs the specified message to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">The level</param>
        public static void Log(string message, LogLevel level = LogLevel.Debug)
        {
            LoggingService.Log(message, level);
        }

        /// <summary>
        /// Logs the specified message and exception to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="level">The level</param>
        public static void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            LoggingService.Log(message, exception, level);
        }

        /// <summary>
        /// Logs an audit message.
        /// </summary>
        /// <param name="entryType">The message type</param>
        /// <param name="message">The message</param>
        public static void LogToAudit(string entryType, string message)
        {
            LoggingService.LogToAudit(entryType, message);
        }

        /// <summary>
        /// Logs analytics data.
        /// </summary>
        /// <param name="entryType">The data type</param>
        /// <param name="data">The data</param>
        public static void LogToAnalytics(string entryType, string data)
        {
            LoggingService.LogToAnalytics(entryType, data);
        }
    }
}