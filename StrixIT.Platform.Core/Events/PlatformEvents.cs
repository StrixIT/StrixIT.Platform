#region Apache License

//-----------------------------------------------------------------------
// <copyright file="PlatformEvents.cs" company="StrixIT">
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
    /// A class to allow for easily raising and consuming events.
    /// </summary>
    public static class PlatformEvents
    {
        #region Public Methods

        /// <summary>
        /// Raises an event for the platform.
        /// </summary>
        /// <typeparam name="T">The type of the event to raise</typeparam>
        /// <param name="args">The event to raise</param>
        public static void Raise<T>(T args) where T : IPlatformEvent
        {
            if (DependencyInjector.Injector != null)
            {
                foreach (var handler in DependencyInjector.GetAll<IHandlePlatformEvent<T>>())
                {
                    handler.Handle(args);
                }
            }
        }

        #endregion Public Methods
    }
}