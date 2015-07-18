#region Apache License
//-----------------------------------------------------------------------
// <copyright file="GetControllerEvent.cs" company="StrixIT">
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
#endregion

using System;
using System.Web.Mvc;
using System.Web.Routing;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An event to allow hooking into the controller resolution of the platform.
    /// </summary>
    public class GetControllerEvent : IPlatformEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetControllerEvent" /> class.
        /// </summary>
        /// <param name="requestContext">The current request context</param>
        /// <param name="controllerType">The type of the controller to get</param>
        /// <param name="controllerFactoryFunc">The default function for creating a controller</param>
        /// <param name="stage">The stage at which controller resolution triggered this event</param>
        public GetControllerEvent(RequestContext requestContext, Type controllerType, Func<RequestContext, Type, IController> controllerFactoryFunc, ControllerResolveStage stage)
        {
            this.RequestContext = requestContext;
            this.ControllerType = controllerType;
            this.DefaultControllerFactoryFunc = controllerFactoryFunc;
            this.Stage = stage;
        }

        /// <summary>
        /// Gets the current request context.
        /// </summary>
        public RequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the type of the controller to get.
        /// </summary>
        public Type ControllerType { get; private set; }

        /// <summary>
        /// Gets the default function for creating a controller.
        /// </summary>
        public Func<RequestContext, Type, IController> DefaultControllerFactoryFunc { get; private set; }

        /// <summary>
        /// Gets the stage at which controller resolution triggered this event.
        /// </summary>
        public ControllerResolveStage Stage { get; private set; }

        /// <summary>
        /// Gets or sets the controller to use.
        /// </summary>
        public IController Controller { get; set; }
    }
}