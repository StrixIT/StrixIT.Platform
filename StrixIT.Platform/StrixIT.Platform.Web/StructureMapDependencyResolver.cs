//-----------------------------------------------------------------------
// <copyright file="StructureMapDependencyResolver.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return DependencyInjector.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DependencyInjector.GetAll(serviceType).Cast<object>();
        }
    }
}