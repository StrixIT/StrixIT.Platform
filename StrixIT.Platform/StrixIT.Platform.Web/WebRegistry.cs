//-----------------------------------------------------------------------
// <copyright file="WebRegistry.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            // Scope all data sources to http or thread local.
            foreach (var type in ModuleManager.LoadedAssemblies.SelectMany(a => a.GetTypes().Where(t => typeof(IDataSource).IsAssignableFrom(t) && t.IsInterface)))
            {
                For(type).LifecycleIs(new HybridLifecycle());
            }

            // Tell StructureMap how to construct the objects for which the HttpContext is needed. A Func<object> is needed, because these have to be
            // created per request by StructureMap.
            For<HttpContextBase>().Use(() => System.Web.HttpContext.Current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null);
        }
    }
}
