//-----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
