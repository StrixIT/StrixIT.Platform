//-----------------------------------------------------------------------
// <copyright file="CoreRegistry.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
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