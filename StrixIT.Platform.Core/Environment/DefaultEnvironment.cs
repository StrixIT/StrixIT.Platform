#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DefaultEnvironment.cs" company="StrixIT">
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