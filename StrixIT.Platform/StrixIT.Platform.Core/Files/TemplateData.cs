//-----------------------------------------------------------------------
// <copyright file="TemplateData.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
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
        public TemplateData() { }

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
