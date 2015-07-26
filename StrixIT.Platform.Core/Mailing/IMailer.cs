#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IMailer.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface for the mailer used by the platform.
    /// </summary>
    public interface IMailer
    {
        /// <summary>
        /// Sends a mail using the smtp server.
        /// </summary>
        /// <param name="fromAddress">The address to send the mail from</param>
        /// <param name="toAddress">The address to send the mail to</param>
        /// <param name="subject">The mail subject</param>
        /// <param name="body">The mail body</param>
        /// <returns>True if the mail was send successfully, false otherwise</returns>
        bool SendMail(string fromAddress, string toAddress, string subject, string body);
    }
}