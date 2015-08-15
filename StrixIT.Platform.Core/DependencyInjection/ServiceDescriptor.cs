using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Core.DependencyInjection
{
    public class ServiceDescriptor
    {
        #region Private Fields

        private Func<object> _factory;
        private Type _implementationType;
        private object _instance;
        private ServiceLifetime _lifetime;
        private Type _serviceType;

        #endregion Private Fields

        #region Public Constructors

        public ServiceDescriptor(Type serviceType, Func<object> factory) : this(serviceType, factory, ServiceLifetime.Unique)
        {
        }

        public ServiceDescriptor(Type serviceType, Func<object> factory, ServiceLifetime lifetime)
        {
            _serviceType = serviceType;
            _factory = factory;
            _lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime) : this(serviceType, (Type)null, lifetime)
        {
        }

        public ServiceDescriptor(Type serviceType, Type implementationType) : this(serviceType, implementationType, ServiceLifetime.Unique)
        {
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            _serviceType = serviceType;
            _implementationType = implementationType;
            _lifetime = lifetime;
        }

        #endregion Public Constructors

        #region Public Properties

        public Func<object> Factory
        {
            get
            {
                return _factory;
            }
        }

        public Type ImplementationType
        {
            get
            {
                return _implementationType;
            }
        }

        public object Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public ServiceLifetime Lifetime
        {
            get
            {
                return _lifetime;
            }
        }

        public Type ServiceType
        {
            get
            {
                return _serviceType;
            }
        }

        #endregion Public Properties
    }
}