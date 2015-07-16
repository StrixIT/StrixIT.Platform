//-----------------------------------------------------------------------
// <copyright file="ISmtpClient.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Mail;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface to abstract away the smtp client.
    /// </summary>
    public interface ISmtpClient : IDisposable
    {
        /// <summary>
        /// Sends a mail using the smtp client.
        /// </summary>
        /// <param name="message">The mail to send</param>
        void Send(MailMessage message);
    }
}
