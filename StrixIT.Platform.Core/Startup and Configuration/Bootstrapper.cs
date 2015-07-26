#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Bootstraps the application by Loading all modules, initializing membership when present and
    /// running all initializers.
    /// </summary>
    public class Bootstrapper
    {
        #region Public Methods

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

        #endregion Public Methods
    }
}