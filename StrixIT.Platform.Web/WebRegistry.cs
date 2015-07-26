#region Apache License

//-----------------------------------------------------------------------
// <copyright file="WebRegistry.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;
using System.Linq;
using System.Web;

namespace StrixIT.Platform.Web
{
    public class WebRegistry : Registry
    {
        #region Public Constructors

        public WebRegistry()
        {
            // Scope all data sources to http or thread local.
            foreach (var type in ModuleManager.LoadedAssemblies.SelectMany(a => a.GetTypes().Where(t => typeof(IDataSource).IsAssignableFrom(t) && t.IsInterface)))
            {
                For(type).LifecycleIs(new HybridLifecycle());
            }

            // Tell StructureMap how to construct the objects for which the HttpContext is needed. A
            // Func<object> is needed, because these have to be created per request by StructureMap.
            For<HttpContextBase>().Use(() => System.Web.HttpContext.Current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null);
        }

        #endregion Public Constructors
    }
}