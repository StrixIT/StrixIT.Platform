#region Apache License

//-----------------------------------------------------------------------
// <copyright file="CacheService.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace StrixIT.Platform.Framework
{
    /// <summary>
    /// A cache wrapper for the .NET ObjectCache.
    /// </summary>
    public class CacheService : ICacheService
    {
        #region Private Fields

        private static readonly ObjectCache Cache = MemoryCache.Default;
        private int _absoluteExpiration = 6;
        private int _slidingExpiration = 0;

        #endregion Private Fields

        #region Public Properties

        public int AbsoluteExpiration
        {
            get
            {
                return this._absoluteExpiration;
            }

            set
            {
                this._absoluteExpiration = value;
            }
        }

        public int SlidingExpiration
        {
            get
            {
                return this._slidingExpiration;
            }

            set
            {
                this._slidingExpiration = value;
            }
        }

        #endregion Public Properties

        #region Public Indexers

        public object this[string key]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return null;
                }

                return Cache[key.ToLower()];
            }

            set
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return;
                }

                CacheItemPolicy policy;

                if (this._absoluteExpiration > 0)
                {
                    policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddHours(this._absoluteExpiration) };
                }
                else
                {
                    policy = new CacheItemPolicy() { SlidingExpiration = new TimeSpan(this._slidingExpiration, 0, 0) };
                }

                Cache.Add(key.ToLower(), value, policy);
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Clear()
        {
            List<string> keys = new List<string>();

            // Get all cache keys
            List<KeyValuePair<string, object>> allObjects = Cache.ToList();

            foreach (KeyValuePair<string, object> pair in allObjects)
            {
                keys.Add(pair.Key);
            }

            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                Cache.Remove(keys[i]);
            }
        }

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            Cache.Remove(key.ToLower());
        }

        #endregion Public Methods
    }
}