//-----------------------------------------------------------------------
// <copyright file="ICacheService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to allow generic access to the cache via cache wrappers.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets or sets the absolute expiration for cache items in hours.
        /// </summary>
        int AbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets the sliding expiration for cache items in hours.
        /// </summary>
        int SlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets a value for the specified key from/in the cache, if the cache contains it.
        /// </summary>
        /// <param name="key">The key to get or set the value for</param>
        /// <returns>The value if available, or NULL</returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Deletes an object from the cache based on a key.
        /// </summary>
        /// <param name="key">The key to use</param>
        void Delete(string key);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();
    }
}