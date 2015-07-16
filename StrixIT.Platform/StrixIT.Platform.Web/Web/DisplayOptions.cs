//-----------------------------------------------------------------------
// <copyright file="DisplayOptions.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A class to show content on platform web pages.
    /// </summary>
    public class DisplayOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptions" /> class.
        /// </summary>
        /// <param name="controller">The content controller</param>
        public DisplayOptions(string controller) : this(null, controller, null, null, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptions" /> class.
        /// </summary>
        /// <param name="controller">The content controller</param>
        /// <param name="url">The content url</param>
        public DisplayOptions(string controller, string url) : this(null, controller, null, url, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptions" /> class.
        /// </summary>
        /// <param name="controller">The content controller</param>
        /// <param name="action">The content action</param>
        /// <param name="url">The content url</param>
        public DisplayOptions(string controller, string action, string url) : this(null, controller, action, url, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptions" /> class.
        /// </summary>
        /// <param name="module">The content module</param>
        /// <param name="controller">The content controller</param>
        /// <param name="action">The content action</param>
        /// <param name="url">The content url</param>
        /// <param name="itemPageUrl">The content item page</param>
        /// <param name="defaultText">The default text if the content controller cannot be found</param>
        public DisplayOptions(string module, string controller, string action, string url, string itemPageUrl, string defaultText)
        {
            this.Module = string.IsNullOrWhiteSpace(module) ? null : module.ToLower() == controller.ToLower() ? null : module;
            this.Controller = controller;
            this.Action = string.IsNullOrWhiteSpace(action) ? "display" : action;
            this.Url = url;
            this.ItemPageUrl = itemPageUrl;
            this.DefaultText = string.IsNullOrWhiteSpace(defaultText) ? string.Format(Resources.Interface.NoContentFound, string.Join("/", controller, action, url)) : defaultText;
        }

        /// <summary>
        /// Gets or sets the name of the module to display content for.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets the name of the content controller.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Gets or sets the name of the content action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the url for which to display the content.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the url for the content item page.
        /// </summary>
        public string ItemPageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this content item has child pages.
        /// </summary>
        public bool HasChildPages { get; set; }

        /// <summary>
        /// Gets or sets the default text to show if the content controller cannot be found.
        /// </summary>
        public string DefaultText { get; set; }
    }
}