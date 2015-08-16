#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IMembershipSettings.cs" company="StrixIT">
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

using StrixIT.Platform.Core.Environment;
using System;

namespace StrixIT.Platform.Framework.Environment
{
    public class NullMembershipSettings : IMembershipSettings
    {
        #region Public Properties

        public Guid AdminId
        {
            get
            {
                return Guid.Empty;
            }
        }

        public Guid ApplicationId
        {
            get
            {
                return Guid.Empty;
            }
        }

        public Guid MainGroupId
        {
            get
            {
                return Guid.Empty;
            }
        }

        #endregion Public Properties
    }
}