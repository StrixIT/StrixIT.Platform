using StrixIT.Platform.Core;
using StrixIT.Platform.Core.DependencyInjection;
using StrixIT.Platform.Core.Environment;
using StrixIT.Platform.Framework.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

                if (!DependencyInjector.GetTypeList(typeof(IMembershipSettings)).Where(t => !t.IsInterface && !t.Equals(typeof(NullMembershipSettings))).Any())
                {
                    serviceList.Add(new ServiceDescriptor(typeof(IMembershipSettings), typeof(NullMembershipSettings), ServiceLifetime.Singleton));
                }
                else
                {
                    serviceList.Add(new ServiceDescriptor(typeof(IMembershipSettings), ServiceLifetime.Singleton));
                }

                serviceList.Add(new ServiceDescriptor(typeof(IEnvironment), typeof(DefaultEnvironment)));

                serviceList.Add(new ServiceDescriptor(typeof(ISmtpClient), typeof(DefaultSmtpClient), ServiceLifetime.Singleton));

                // Scope all data sources to http or thread local.
                foreach (var type in DependencyInjector.GetLoadedAssemblies().SelectMany(a => a.GetTypes().Where(t => typeof(IDataSource).IsAssignableFrom(t) && t.IsInterface)))
                {
                    serviceList.Add(new ServiceDescriptor(type, ServiceLifetime.PerRequest));
                }

                // Tell StructureMap how to construct the objects for which the HttpContext is
                // needed. A Func<object> is needed, because these have to be created per request by StructureMap.
                serviceList.Add(new ServiceDescriptor(typeof(HttpContextBase), () => System.Web.HttpContext.Current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null));

                Func<bool> isLocalFunc = () =>
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

                var constructorValues = new List<ConstructorValue<bool>>()
                {
                    new ConstructorValue<bool>("isLocalRequest", Helpers.FuncToExpression(isLocalFunc))
                };

                var configServiceDescriptor = new ServiceDescriptorWithConstructorValues<bool>(typeof(IConfiguration), typeof(Configuration), constructorValues);
                serviceList.Add(configServiceDescriptor);

                return serviceList;
            }
        }

        #endregion Public Properties
    }
}