﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ClientResourceCollection.cs" company="StrixIT">
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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A class to collect resources (strings from resource files and localized enums) to send to
    /// the client.
    /// </summary>
    [Serializable]
    public class ClientResourceCollection : Dictionary<string, IDictionary<string, string>>
    {
        #region Public Constructors

        public ClientResourceCollection()
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected ClientResourceCollection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}