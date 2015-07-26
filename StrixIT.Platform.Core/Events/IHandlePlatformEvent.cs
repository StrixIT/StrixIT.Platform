#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IHandlePlatformEvent.cs" company="StrixIT">
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
    /// An interface to handle events raised by the platform.
    /// </summary>
    /// <typeparam name="T">The type of the event the implementor handles</typeparam>
    public interface IHandlePlatformEvent<T> where T : IPlatformEvent
    {
        #region Public Methods

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="args">The event to handle</param>
        void Handle(T args);

        #endregion Public Methods
    }
}