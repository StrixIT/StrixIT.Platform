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

using System;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;

namespace StrixIT.Platform.Core
{
    public class Mailer : IMailer
    {
        private ISmtpClient _smtpClient;

        public Mailer(ISmtpClient smtpClient)
        {
            this._smtpClient = smtpClient;
        }

        public bool SendMail(string fromAddress, string toAddress, string subject, string body)
        {
            var mail = new MailMessage(fromAddress, toAddress, subject, body);
            mail.IsBodyHtml = true;
            bool success = false;
            var mailSettings = Helpers.GetConfigSectionGroup<MailSettingsSectionGroup>("system.net/mailSettings");
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
                this._smtpClient.Send(mail);
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
    }
}