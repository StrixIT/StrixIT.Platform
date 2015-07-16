//-----------------------------------------------------------------------
// <copyright file="WebAssemblyLoader.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Compilation;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    public class WebAssemblyLoader
    {
        public static void LoadAssemblies()
        {
            ModuleManager.LoadAssemblies();

            foreach (var assembly in ModuleManager.LoadedAssemblies)
            {
                BuildManager.AddReferencedAssembly(assembly);
            }
        }
    }
}