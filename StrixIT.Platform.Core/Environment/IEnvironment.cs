#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="StrixIT">
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

using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to abstract away the environment the application is running in.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets the email of the user currently logged in.
        /// </summary>
        string CurrentUserEmail { get; }

        /// <summary>
        /// Gets the working directory (the application root folder).
        /// </summary>
        string WorkingDirectory { get; }

        /// <summary>
        /// Gets an object with the specified key from the user's session.
        /// </summary>
        /// <typeparam name="T">The type of the object to return</typeparam>
        /// <param name="key">The session key to use</param>
        /// <returns>The requested object when found, or the default value for that type when not found</returns>
        T GetFromSession<T>(string key);

        /// <summary>
        /// Stores an object in the user's session using the key specified.
        /// </summary>
        /// <param name="key">The session key to use</param>
        /// <param name="theObject">The object to store</param>
        void StoreInSession(string key, object theObject);

        /// <summary>
        /// Abandons the user's session.
        /// </summary>
        void AbandonSession();

        /// <summary>
        /// Gets a dictionary holding all objects in the user's session.
        /// </summary>
        /// <returns>The session value dictionary</returns>
        IDictionary<string, object> GetSessionDictionary();

        /// <summary>
        /// Maps a path to the environment.
        /// </summary>
        /// <param name="path">The path to map</param>
        /// <returns>The mapped path</returns>
        string MapPath(string path);
    }
}