#region Apache License
//-----------------------------------------------------------------------
// <copyright file="CoreRegistry.cs" company="StrixIT">
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

using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The Structuremap registry for the playform core.
    /// </summary>
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            this.Scan(x =>
            {
                foreach (var assembly in ModuleManager.LoadedAssemblies)
                {
                    x.Assembly(assembly);
                }

                x.AddAllTypesOf<IInitializer>();
                x.AddAllTypesOf<IWebInitializer>();
                x.ConnectImplementationsToTypesClosing(typeof(IHandlePlatformEvent<>));
                x.WithDefaultConventions();
            });

            For<ISmtpClient>().Use<DefaultSmtpClient>();
        }
    }
}