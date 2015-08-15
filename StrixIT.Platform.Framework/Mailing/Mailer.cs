#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Mailer.cs" company="StrixIT">
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
using System;
using System.IO;
using System.Net.Mail;

namespace StrixIT.Platform.Framework
{
    public class Mailer : IMailer
    {
        #region Private Fields

        private IConfiguration _config;
        private ISmtpClient _smtpClient;

        #endregion Private Fields

        #region Public Constructors

        public Mailer(ISmtpClient smtpClient, IConfiguration config)
        {
            _smtpClient = smtpClient;
            _config = config;
        }

        #endregion Public Constructors

        #region Public Methods

        public bool SendMail(string fromAddress, string toAddress, string subject, string body)
        {
            var mail = new MailMessage(fromAddress, toAddress, subject, body);
            mail.IsBodyHtml = true;
            bool success = false;
            var mailSettings = _config.GetConfigSectionGroup("system.net/mailSettings");
            var pickupDir = mailSettings != null &&
                            mailSettings.Smtp != null &&
                            mailSettings.Smtp.SpecifiedPickupDirectory != null ?
                            mailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation : null;

            if (!string.IsNullOrWhiteSpace(pickupDir) && !Directory.Exists(pickupDir))
            {
                Directory.CreateDirectory(pickupDir);
            }

            try
            {
                _smtpClient.Send(mail);
                success = true;
                mail.Dispose();
            }
            catch (Exception ex)
            {
                mail.Dispose();
                Logger.Log(string.Format("An error occurred while trying to send a mail with subject {0} to {1}.", mail.Subject, mail.To), ex, LogLevel.Error);

                throw;
            }

            return success;
        }

        #endregion Public Methods
    }
}