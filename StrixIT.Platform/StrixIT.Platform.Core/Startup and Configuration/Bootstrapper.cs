//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Bootstraps the application by Loading all modules, initializing membership when present and running all initializers.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Loads all modules, initializes membership when present and runs all initializers.
        /// </summary>
        public static void Run()
        {
                StrixPlatform.WriteStartupMessage("Application bootstrap start. Load assemblies if still required.");
                ModuleManager.LoadAssemblies();

                StrixPlatform.WriteStartupMessage("Load module configurations.");
                ModuleManager.LoadConfigurations();

                // Initialize membership data
                var membershipService = DependencyInjector.TryGet<IMembershipService>();

                if (membershipService != null)
                {
                    StrixPlatform.WriteStartupMessage("Initialize membership");
                    membershipService.Initialize();
                }

                StrixPlatform.WriteStartupMessage("Run all application initializers");

                foreach (var initializer in DependencyInjector.GetAll<IInitializer>())
                {
                    StrixPlatform.WriteStartupMessage(string.Format("Start initializer {0}.", initializer.GetType().Name));
                    initializer.Initialize();
                }

                StrixPlatform.WriteStartupMessage("Application boostrap finished.");
        }
    }
}
