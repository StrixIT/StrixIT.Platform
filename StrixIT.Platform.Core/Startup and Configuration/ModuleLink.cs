#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ModuleLink.cs" company="StrixIT">
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
    /// A class to define a link for a content type for a module from the administration dashboard.
    /// </summary>
    public class ModuleLink
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink"/> class.
        /// </summary>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string controllerName) : this(controllerName, null, controllerName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink"/> class.
        /// </summary>
        /// <param name="requiredPermission">The permission required to view and access the link</param>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string requiredPermission, string controllerName) : this(controllerName, requiredPermission, controllerName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink"/> class.
        /// </summary>
        /// <param name="title">The link text</param>
        /// <param name="requiredPermission">The permission required to view and access the link</param>
        /// <param name="controllerName">The name of the controller for the link</param>
        public ModuleLink(string title, string requiredPermission, string controllerName) : this(title, requiredPermission, controllerName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLink"/> class.
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

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the action name for the link.
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// Gets the controller name for the link.
        /// </summary>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the permission required to see and access this link.
        /// </summary>
        public string RequiredPermission { get; private set; }

        /// <summary>
        /// Gets the link text.
        /// </summary>
        public string Title { get; private set; }

        #endregion Public Properties
    }
}