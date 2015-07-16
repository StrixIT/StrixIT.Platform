//-----------------------------------------------------------------------
// <copyright file="ModuleLink.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to define a link for a content type for a module from the administration dashboard.
    /// </summary>
    public class ModuleLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink" /> class.
        /// </summary>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string controllerName) : this(controllerName, null, controllerName, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink" /> class.
        /// </summary>
        /// <param name="requiredPermission">The permission required to view and access the link</param>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string requiredPermission, string controllerName) : this(controllerName, requiredPermission, controllerName, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink" /> class.
        /// </summary>
        /// <param name="title">The link text</param>
        /// <param name="requiredPermission">The permission required to view and access the link</param>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string title, string requiredPermission, string controllerName) : this(title, requiredPermission, controllerName, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink" /> class.
        /// </summary>
        /// <param name="title">The link text</param>
        /// <param name="requiredPermission">The permission required to view and access the link</param>
        /// <param name="controllerName">The name of the controller for the link</param>
        /// <param name="actionName">The name of the action for the link</param>
        public ModuleLink(string title, string requiredPermission, string controllerName, string actionName)
        {
            this.Title = title;
            this.RequiredPermission = requiredPermission;
            this.ControllerName = controllerName;
            this.ActionName = actionName != null ? actionName : "index";
        }

        /// <summary>
        /// Gets the link text.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the permission required to see and access this link.
        /// </summary>
        public string RequiredPermission { get; private set; }

        /// <summary>
        /// Gets the controller name for the link.
        /// </summary>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the action name for the link.
        /// </summary>
        public string ActionName { get; private set; }
    }
}