//-----------------------------------------------------------------------
// <copyright file="ILoggingService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface for the Logging service.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Gets the url at which to log script errors for web applications.
        /// </summary>
        string LogScriptErrorUrl { get; }

        /// <summary>
        /// Logs the specified message to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">The level</param>
        void Log(string message, LogLevel level = LogLevel.Debug);

        /// <summary>
        /// Logs the specified message and exception to the log.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="level">The level</param>
        void Log(string message, Exception exception, LogLevel level = LogLevel.Debug);

        /// <summary>
        /// Logs an audit message.
        /// </summary>
        /// <param name="entryType">The message type</param>
        /// <param name="message">The message</param>
        void LogToAudit(string entryType, string message);

        /// <summary>
        /// Logs analytics data.
        /// </summary>
        /// <param name="entryType">The data type</param>
        /// <param name="data">The data</param>
        void LogToAnalytics(string entryType, string data);
    }
}