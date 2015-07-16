//-----------------------------------------------------------------------
// <copyright file="Mailer.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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