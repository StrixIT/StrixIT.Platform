//-----------------------------------------------------------------------
// <copyright file="GetContentEvent.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An event to get custom content for the Html Content helper.
    /// </summary>
    public class GetContentEvent : IPlatformEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetContentEvent" /> class.
        /// </summary>
        /// <param name="helper">The HtmlHelper used</param>
        /// <param name="options">The display options passed to the content helper</param>
        public GetContentEvent(HtmlHelper helper, DisplayOptions options)
        {
            this.Helper = helper;
            this.Options = options;
        }

        /// <summary>
        /// Gets the HtmlHelper to use.
        /// </summary>
        public HtmlHelper Helper { get; private set; }

        /// <summary>
        /// Gets the display options passed to the Content helper.
        /// </summary>
        public DisplayOptions Options { get; private set; }

        /// <summary>
        /// Gets or sets the custom content result.
        /// </summary>
        public MvcHtmlString Result { get; set; }
    }
}