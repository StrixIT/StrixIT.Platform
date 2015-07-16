//-----------------------------------------------------------------------
// <copyright file="GeneralEvent.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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