#region Apache License

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

using StrixIT.Platform.Core;
using StrixIT.Platform.Core.DependencyInjection;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StrixIT.Platform.Framework
{
    public class StructureMapDependencyInjector : IDependencyInjector
    {
        #region Private Fields

        private static IContainer _container;
        private static bool _initialized = false;
        private static object _lockObject = new object();

        #endregion Private Fields

        #region Public Constructors

        public StructureMapDependencyInjector()
        {
            if (!_initialized)
            {
                lock (_lockObject)
                {
                    if (!_initialized)
                    {
                        _container = CreateContainer();
                        _initialized = true;
                    }
                }
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

        #region Private Methods

        private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> f)
        {
            return x => f(x);
        }

        private IContainer CreateContainer()
        {
            return new Container(x =>
            {
                x.Scan(scanner =>
                {
                    foreach (var assembly in DependencyInjector.GetLoadedAssemblies())
                    {
                        scanner.Assembly(assembly);
                    }

                    scanner.AddAllTypesOf<IInitializer>();
                    scanner.AddAllTypesOf<IWebInitializer>();
                    scanner.AddAllTypesOf<IModuleConfiguration>();
                    scanner.AddAllTypesOf<IWebModuleConfiguration>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IHandlePlatformEvent<>));
                    scanner.WithDefaultConventions();
                });

                Func<IContext, bool> isLocalFunc = i =>
                {
                    try
                    {
                        return HttpContext.Current != null && HttpContext.Current.Request != null ? HttpContext.Current.Request.IsLocal : true;
                    }
                    catch
                    {
                        return true;
                    }
                };

                x.For<IConfiguration>().Use<Configuration>().Ctor<bool>("isLocalRequest").Is(FuncToExpression(isLocalFunc));

                var descriptors = GetObjectList<IServiceConfiguration>();

                foreach (var descriptor in descriptors.SelectMany(d => d.Services))
                {
                    var lifeCycle = GetLifeCycle(descriptor.Lifetime);

                    if (descriptor.Instance != null)
                    {
                        x.For(descriptor.ServiceType).Use(descriptor.Instance).SetLifecycleTo(lifeCycle);
                    }
                    else if (descriptor.Factory != null)
                    {
                        x.For(descriptor.ServiceType).Use(c => descriptor.Factory()).SetLifecycleTo(lifeCycle);
                    }
                    else if (descriptor.ImplementationType != null)
                    {
                        x.For(descriptor.ServiceType).Use(descriptor.ImplementationType).SetLifecycleTo(lifeCycle);
                    }
                    else
                    {
                        x.For(descriptor.ServiceType).LifecycleIs(lifeCycle);
                    }
                }
            });
        }

        private ILifecycle GetLifeCycle(ServiceLifetime lifetime)
        {
            Type lifeCycleType;

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    {
                        lifeCycleType = typeof(SingletonLifecycle);
                    }
                    break;

                case ServiceLifetime.PerRequest:
                    {
                        lifeCycleType = typeof(HybridLifecycle);
                    }
                    break;

                default:
                    {
                        lifeCycleType = typeof(UniquePerRequestLifecycle);
                    }
                    break;
            }

            return Activator.CreateInstance(lifeCycleType) as ILifecycle;
        }

        private IList<T> GetObjectList<T>()
        {
            var list = new List<T>();
            var types = DependencyInjector.GetTypeList(typeof(T));

            foreach (var type in types)
            {
                if (!type.IsAbstract)
                {
                    list.Add((T)Activator.CreateInstance(type));
                }
            }

            return list;
        }

        #endregion Private Methods
    }
}