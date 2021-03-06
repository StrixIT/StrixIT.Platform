﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="StructureMapDependencyInjector.cs" company="StrixIT">
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

using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    public class StructureMapDependencyInjector : IDependencyInjector
    {
        #region Private Fields

        private static IContainer _container;
        private static bool _initialized = false;

        #endregion Private Fields

        #region Public Constructors

        public StructureMapDependencyInjector()
        {
            if (!_initialized)
            {
                var registries = ModuleManager.GetObjectList<Registry>();

                var container = new Container(x =>
                {
                    foreach (var registry in registries)
                    {
                        x.AddRegistry(registry);
                    }
                });

                _container = container;
                _initialized = true;
            }
        }

        #endregion Public Constructors

        #region Public Methods

        public T Get<T>()
        {
            return _container.GetInstance<T>();
        }

        public object Get(Type dependencyType)
        {
            return _container.GetInstance(dependencyType);
        }

        public T Get<T>(string key)
        {
            return _container.GetInstance<T>(key);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _container.GetAllInstances<T>();
        }

        public IEnumerable GetAll(Type dependencyType)
        {
            return _container.GetAllInstances(dependencyType);
        }

        public T TryGet<T>()
        {
            return _container.TryGetInstance<T>();
        }

        public object TryGet(Type dependencyType)
        {
            return _container.TryGetInstance(dependencyType);
        }

        public T TryGet<T>(string key)
        {
            return _container.TryGetInstance<T>(key);
        }

        #endregion Public Methods
    }
}