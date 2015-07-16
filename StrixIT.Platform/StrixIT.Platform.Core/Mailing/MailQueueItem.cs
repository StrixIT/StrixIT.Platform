//-----------------------------------------------------------------------
// <copyright file="MailQueueItem.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to resend mails later when the first attempt fails.
    /// </summary>
    public class MailQueueItem : ValidationBase
    {
        public int Id { get; set; }

        [StrixRequired]
        [StringLength(250)]
        public string From { get; set; }

        [StrixRequired]
        public string To { get; set; }

        [StrixRequired]
        [StringLength(250)]
        public string Subject { get; set; }

        [StrixRequired]
        public string Body { get; set; }

        [StrixRequired]
        public DateTime FirstSendAttempt { get; set; }
    }
}
