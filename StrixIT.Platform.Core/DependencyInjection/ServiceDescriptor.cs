﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ImageAttribute.cs" company="StrixIT">
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
using System.Collections.Generic;

namespace StrixIT.Platform.Core.DependencyInjection
{
    public class ServiceDescriptor
    {
        #region Public Constructors

        public ServiceDescriptor(Type serviceType, Func<object> factory) : this(serviceType, factory, ServiceLifetime.Unique)
        {
        }

        public ServiceDescriptor(Type serviceType, Func<object> factory, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            Factory = factory;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime) : this(serviceType, (Type)null, lifetime)
        {
        }

        public ServiceDescriptor(Type serviceType, Type implementationType) : this(serviceType, implementationType, ServiceLifetime.Unique)
        {
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        #endregion Public Constructors

        #region Public Properties

        public Func<object> Factory { get; private set; }

        public Type ImplementationType { get; private set; }

        public object Instance { get; private set; }

        public ServiceLifetime Lifetime { get; private set; }

        public Type ServiceType { get; private set; }

        #endregion Public Properties
    }
}