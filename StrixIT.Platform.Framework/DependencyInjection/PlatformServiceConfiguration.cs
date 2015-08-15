using StrixIT.Platform.Core;
using StrixIT.Platform.Core.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Framework
{
    public class PlatformServiceConfiguration : IServiceConfiguration
    {
        #region Public Properties

        public IList<ServiceDescriptor> Services
        {
            get
            {
                var serviceList = new List<ServiceDescriptor>();

                if (!DependencyInjector.GetTypeList(typeof(IUserContext)).Where(t => !t.IsInterface && !t.Equals(typeof(NullUserContext))).Any())
                {
                    serviceList.Add(new ServiceDescriptor(typeof(IUserContext), typeof(NullUserContext), ServiceLifetime.Singleton));
                }
                else
                {
                    serviceList.Add(new ServiceDescriptor(typeof(IUserContext), ServiceLifetime.PerRequest));
                }

                serviceList.Add(new ServiceDescriptor(typeof(ISmtpClient), typeof(DefaultSmtpClient), ServiceLifetime.Singleton));

                return serviceList;
            }
        }

        #endregion Public Properties
    }
}