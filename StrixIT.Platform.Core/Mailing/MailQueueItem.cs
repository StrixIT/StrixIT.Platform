#region Apache License

//-----------------------------------------------------------------------
// <copyright file="MailQueueItem.cs" company="StrixIT">
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
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to resend mails later when the first attempt fails.
    /// </summary>
    public class MailQueueItem : ValidationBase
    {
        #region Public Properties

        [StrixRequired]
        public string Body { get; set; }

        [StrixRequired]
        public DateTime FirstSendAttempt { get; set; }

        [StrixRequired]
        [StringLength(250)]
        public string From { get; set; }

        public int Id { get; set; }

        [StrixRequired]
        [StringLength(250)]
        public string Subject { get; set; }

        [StrixRequired]
        public string To { get; set; }

        #endregion Public Properties
    }
}