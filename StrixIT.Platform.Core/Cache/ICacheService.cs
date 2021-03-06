﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ICacheService.cs" company="StrixIT">
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
    /// An interface to allow generic access to the cache via cache wrappers.
    /// </summary>
    public interface ICacheService
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the absolute expiration for cache items in hours.
        /// </summary>
        int AbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets the sliding expiration for cache items in hours.
        /// </summary>
        int SlidingExpiration { get; set; }

        #endregion Public Properties

        #region Public Indexers

        /// <summary>
        /// Gets or sets a value for the specified key from/in the cache, if the cache contains it.
        /// </summary>
        /// <param name="key">The key to get or set the value for</param>
        /// <returns>The value if available, or NULL</returns>
        object this[string key] { get; set; }

        #endregion Public Indexers

        #region Public Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Deletes an object from the cache based on a key.
        /// </summary>
        /// <param name="key">The key to use</param>
        void Delete(string key);

        #endregion Public Methods
    }
}