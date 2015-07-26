#region Apache License
//-----------------------------------------------------------------------
// <copyright file="IWebModuleConfiguration.cs" company="StrixIT">
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

using System.Collections.Generic;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The interface to configure modules that need to register additional web components.
    /// </summary>
    public interface IWebModuleConfiguration : IModuleConfiguration
    {
        /// <summary>
        /// Gets the style bundles this module uses.
        /// </summary>
        IList<string> StyleBundles { get; }

        /// <summary>
        /// Gets the script bundles this module uses.
        /// </summary>
        IList<string> ScriptBundles { get; }
    }
}
