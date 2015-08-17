#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IDependencyInjector.cs" company="StrixIT">
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
using System.Collections;
using System.Collections.Generic;

namespace StrixIT.Platform.Core.DependencyInjection
{
    /// <summary>
    /// The interface to abstract away the Dependency Injection implementation.
    /// </summary>
    public interface IDependencyInjector
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <returns>The instance</returns>
        T Get<T>();

        /// <summary>
        /// Gets an instance of the class of the specified type.
        /// </summary>
        /// <param name="dependencyType">The type to get an instance of</param>
        /// <returns>The instance</returns>
        object Get(Type dependencyType);

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

        /// <summary>
        /// Tries to get an instance of the class of type T.
        /// </summary>
        /// <typeparam name="T">The type to get an instance of</typeparam>
        /// <returns>The instance, or NULL if it was not found</returns>
        T TryGet<T>();

        /// <summary>
        /// Tries to get an instance of the class of the specified type.
        /// </summary>
        /// <param name="dependencyType">The type to get an instance of</param>
        /// <returns>The instance, or NULL if it was not found</returns>
        object TryGet(Type dependencyType);

        #endregion Public Methods
    }
}