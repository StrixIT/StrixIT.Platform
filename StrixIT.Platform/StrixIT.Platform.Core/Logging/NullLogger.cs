//-----------------------------------------------------------------------
// <copyright file="NullLogger.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    public class NullLogger : ILoggingService
    {
        public string LogScriptErrorUrl
        {
            get
            {
                return null;
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Debug)
        {
            return;
        }

        public void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            return;
        }

        public void LogToAudit(string type, string message)
        {
            return;
        }

        public void LogToAnalytics(string entryType, string data)
        {
            return;
        }
    }
}