#region Apache License
//-----------------------------------------------------------------------
// <copyright file="StructureMapControllerFactory.cs" company="StrixIT">
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