//-----------------------------------------------------------------------
// <copyright file="ClientResourceCollection.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A class to collect resources (strings from resource files and localized enums) to send to the client.
    /// </summary>
    [Serializable]
    public class ClientResourceCollection : Dictionary<string, IDictionary<string, string>> 
    {
        public ClientResourceCollection() { }

        protected ClientResourceCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
