#region Apache License

//-----------------------------------------------------------------------
// <copyright file="TemplateData.cs" company="StrixIT">
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
    /// A class to store template data.
    /// </summary>
    public class TemplateData
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="TemplateData" /> class.
        /// </summary>
        public TemplateData()
        {
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="TemplateData" /> class.
        /// </summary>
        /// <param name="culture">The template culture</param>
        /// <param name="subject">The template subject</param>
        /// <param name="body">The template body</param>
        public TemplateData(string culture, string subject, string body)
        {
            this.Culture = culture;
            this.Subject = subject;
            this.Body = body;
        }

        /// <summary>
        /// Gets or sets the culture for the template.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the subject for the template.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body for the template.
        /// </summary>
        public string Body { get; set; }
    }
}