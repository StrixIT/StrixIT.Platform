//-----------------------------------------------------------------------
// <copyright file="IDependencyInjector.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface to abstract away the Dependency Injection implementation.
    /// </summary>
    public interface IDependencyInjector
    {
        /// <summary>
        /// Gets an instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <returns>The instance</returns>
        T Get<T>();

        /// <summary>
        /// Tries to get an instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <returns>The instance, or NULL if it was not found</returns>
        T TryGet<T>();

        /// <summary>
        /// Gets an instance of the class of the specified type.
        /// </summary>
        /// <param name="dependencyType">The type to get an instance of</param>
        /// <returns>The instance</returns>
        object Get(Type dependencyType);

        /// <summary>
        /// Tries to get an instance of the class of the specified type.
        /// </summary>
        /// <param name="dependencyType">The type to get an instance of</param>
        /// <returns>The instance, or NULL if it was not found</returns>
        object TryGet(Type dependencyType);

        /// <summary>
        /// Gets a named instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <param name="key">The instance name</param>
        /// <returns>The instance</returns>
        T Get<T>(string key);

        /// <summary>
        /// Tries to get a named instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <param name="key">The instance name</param>
        /// <returns>The instance, or NULL if it was not found</returns>
        T TryGet<T>(string key);

        /// <summary>
        /// Gets an instance of all classes of type T.
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>An instance of all classes of type T</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Gets an instance of all classes of the specified type.
        /// </summary>
        /// <param name="dependencyType">The type of the classes to get</param>
        /// <returns>An instance of all classes of the specified type</returns>
        IEnumerable GetAll(Type dependencyType);
    }
}