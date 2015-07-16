//-----------------------------------------------------------------------
// <copyright file="IMailer.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
