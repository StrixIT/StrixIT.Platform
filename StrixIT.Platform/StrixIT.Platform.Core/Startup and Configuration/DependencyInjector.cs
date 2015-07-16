//-----------------------------------------------------------------------
// <copyright file="DependencyInjector.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    public static class DependencyInjector
    {
        private static IDependencyInjector _dependencyInjector;
        private static IDependencyInjector _defaultInjector;

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

        public static T Get<T>()
        {
            return Injector.Get<T>();
        }

        public static T Get<T>(string instanceKey)
        {
            return Injector.Get<T>(instanceKey);
        }

        public static T TryGet<T>()
        {
            return Injector.TryGet<T>();
        }

        public static T TryGet<T>(string instanceKey)
        {
            return Injector.TryGet<T>(instanceKey);
        }

        public static object Get(Type type)
        {
            return Injector.Get(type);
        }

        public static object TryGet(Type dependencyType)
        {
            return Injector.TryGet(dependencyType);
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return Injector.GetAll<T>();
        }

        public static IEnumerable GetAll(Type dependencyType)
        {
            return Injector.GetAll(dependencyType);
        }
    }
}