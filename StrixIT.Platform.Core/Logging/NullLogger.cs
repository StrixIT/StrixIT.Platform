#region Apache License

//-----------------------------------------------------------------------
// <copyright file="NullLogger.cs" company="StrixIT">
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
    public class NullLogger : ILoggingService
    {
        #region Public Properties

        public string LogScriptErrorUrl
        {
            get
            {
                return null;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Log(string message, LogLevel level = LogLevel.Debug)
        {
            return;
        }

        public void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            return;
        }

        public void LogToAnalytics(string entryType, string data)
        {
            return;
        }

        public void LogToAudit(string type, string message)
        {
            return;
        }

        #endregion Public Methods
    }
}