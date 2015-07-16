//-----------------------------------------------------------------------
// <copyright file="DefaultSmtpClient.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Mail;

namespace StrixIT.Platform.Core
{
    public class DefaultSmtpClient : SmtpClient, ISmtpClient
    {
        public new void Send(MailMessage message)
        {
            base.Send(message);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}