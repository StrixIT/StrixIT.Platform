#region Apache License
//-----------------------------------------------------------------------
// <copyright file="GeneralEvent.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to raise and handle events without knowlegde of the sender.
    /// </summary>
    public class GeneralEvent : IPlatformEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralEvent" /> class.
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="data">The event data</param>
        public GeneralEvent(string eventName, IDictionary<string, object> data)
        {
            this.EventName = eventName;
            this.Data = data;
        }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// Gets the data of the event.
        /// </summary>
        public IDictionary<string, object> Data { get; private set; }
    }
}