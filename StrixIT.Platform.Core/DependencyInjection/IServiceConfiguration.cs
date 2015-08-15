using System.Collections.Generic;

namespace StrixIT.Platform.Core.DependencyInjection
{
    public interface IServiceConfiguration
    {
        #region Public Properties

        IList<ServiceDescriptor> Services { get; }

        #endregion Public Properties
    }
}