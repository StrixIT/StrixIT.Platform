//-----------------------------------------------------------------------
// <copyright file="StructureMapControllerFactory.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The structuremap controller factory.
    /// </summary>
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // Allow hooking in to the controller retrieval process.
            var findControllerEvent = new GetControllerEvent(requestContext, controllerType, base.GetControllerInstance, ControllerResolveStage.Find);
            StrixPlatform.RaiseEvent(findControllerEvent);

            if (findControllerEvent.Controller != null)
            {
                return findControllerEvent.Controller;
            }

            if (controllerType != null)
            {
                return DependencyInjector.Get(controllerType) as IController;
            }

            try
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            catch (HttpException ex)
            {
                if (ex.GetHttpCode() == 404)
                {
                    // Allow hooking in when no controller is found.
                    var notFoundControllerEvent = new GetControllerEvent(requestContext, controllerType, base.GetControllerInstance, ControllerResolveStage.NotFound);
                    StrixPlatform.RaiseEvent(notFoundControllerEvent);

                    if (notFoundControllerEvent.Controller != null)
                    {
                        return notFoundControllerEvent.Controller;
                    }
                }

                throw;
            }
        }
    }
}