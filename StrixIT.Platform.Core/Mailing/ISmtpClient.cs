#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ISmtpClient.cs" company="StrixIT">
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
using System.Net.Mail;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface to abstract away the smtp client.
    /// </summary>
    public interface ISmtpClient : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Sends a mail using the smtp client.
        /// </summary>
        /// <param name="message">The mail to send</param>
        void Send(MailMessage message);

        #endregion Public Methods
    }
}