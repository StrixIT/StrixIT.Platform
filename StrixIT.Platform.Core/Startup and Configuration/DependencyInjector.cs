#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DependencyInjector.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    public static class DependencyInjector
    {
        #region Private Fields

        private static IDependencyInjector _defaultInjector;
        private static IDependencyInjector _dependencyInjector;

        #endregion Private Fields

        #region Public Properties

        public static IDependencyInjector Injector
        {
            get
            {
                if (_dependencyInjector == null)
                {
                    if (_defaultInjector == null)
                    {
                        var typeParts = StrixPlatform.Configuration.DependencyInjector.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var type = ModuleManager.GetObjectTypeByFullName(string.Format("{0}.{1}", typeParts[0].Trim(), typeParts[1].Trim()));
                        _defaultInjector = Activator.CreateInstance(type) as IDependencyInjector;
                    }

                    return _defaultInjector;
                }

                return _dependencyInjector;
            }
            set
            {
                _dependencyInjector = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static T Get<T>()
        {
            return Injector.Get<T>();
        }

        public static T Get<T>(string instanceKey)
        {
            return Injector.Get<T>(instanceKey);
        }

        public static object Get(Type type)
        {
            return Injector.Get(type);
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return Injector.GetAll<T>();
        }

        public static IEnumerable GetAll(Type dependencyType)
        {
            return Injector.GetAll(dependencyType);
        }

        public static T TryGet<T>()
        {
            return Injector.TryGet<T>();
        }

        public static T TryGet<T>(string instanceKey)
        {
            return Injector.TryGet<T>(instanceKey);
        }

        public static object TryGet(Type dependencyType)
        {
            return Injector.TryGet(dependencyType);
        }

        #endregion Public Methods
    }
}