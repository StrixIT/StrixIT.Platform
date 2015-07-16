//-----------------------------------------------------------------------
// <copyright file="CacheService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A cache wrapper for the .NET ObjectCache.
    /// </summary>
    public class CacheService : ICacheService
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private int _absoluteExpiration = 6;
        private int _slidingExpiration = 0;

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

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            Cache.Remove(key.ToLower());
        }

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
    }
}