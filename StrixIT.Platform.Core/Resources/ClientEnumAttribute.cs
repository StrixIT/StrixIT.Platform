#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ClientEnumAttribute.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An attribute to mark an enum as an enum that should be loaded to the client.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ClientEnumAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the resource to use to localize the enum.
        /// </summary>
        public Type ResourceType { get; set; }
    }
}
