//-----------------------------------------------------------------------
// <copyright file="GetControllerEvent.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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