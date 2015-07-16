//-----------------------------------------------------------------------
// <copyright file="DefaultEnvironment.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace StrixIT.Platform.Core
{
    public class DefaultEnvironment : IEnvironment
    {
        public string CurrentUserEmail
        {
            get
            {
                return null;
            }
        }

        public string WorkingDirectory
        {
            get 
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToLower();

                if (path.Contains("\\bin\\"))
                {
                    path = path.Substring(0, path.IndexOf("\\bin\\"));
                }

                return path;
            }
        }

        public string MapPath(string path)
        {
            if (path.Contains("/"))
            {
                return Path.Combine(this.WorkingDirectory, path.Replace("/", "\\"));
            }

            return path;
        }

        public T GetFromSession<T>(string key)
        {
            return default(T);
        }

        public void StoreInSession(string key, object theObject)
        {
            return;
        }

        public void AbandonSession()
        {
            return;
        }

        public IDictionary<string, object> GetSessionDictionary()
        {
            return new Dictionary<string, object>();
        }
    }
}