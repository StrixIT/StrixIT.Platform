#region Apache License

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
    public class ServiceDescriptorWithConstructorValue<T> : ServiceDescriptor
    {
        #region Public Constructors

        public ServiceDescriptorWithConstructorValue(Type serviceType, Type implementationType, ConstructorValue<T> value) : this(serviceType, implementationType, ServiceLifetime.Unique, value)
        {
        }

        public ServiceDescriptorWithConstructorValue(Type serviceType, Type implementationType, ServiceLifetime lifetime, ConstructorValue<T> value) : base(serviceType, implementationType, lifetime)
        {
            ConstructorValue = value;
        }

        #endregion Public Constructors

        #region Public Properties

        public ConstructorValue<T> ConstructorValue { get; set; }

        #endregion Public Properties
    }
}