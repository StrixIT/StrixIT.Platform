//-----------------------------------------------------------------------
// <copyright file="StructureMapDependencyInjector.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace StrixIT.Platform.Core
{
    public class StructureMapDependencyInjector : IDependencyInjector
    {
        private static bool _initialized = false;
        private static IContainer _container;

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

        public T Get<T>()
        {
            return _container.GetInstance<T>();
        }

        public T TryGet<T>()
        {
            return _container.TryGetInstance<T>();
        }

        public object Get(Type dependencyType)
        {
            return _container.GetInstance(dependencyType);
        }

        public object TryGet(Type dependencyType)
        {
            return _container.TryGetInstance(dependencyType);
        }

        public T Get<T>(string key)
        {
            return _container.GetInstance<T>(key);
        }

        public T TryGet<T>(string key)
        {
            return _container.TryGetInstance<T>(key);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _container.GetAllInstances<T>();
        }

        public IEnumerable GetAll(Type dependencyType)
        {
            return _container.GetAllInstances(dependencyType);
        }
    }
}