using System;
using System.ServiceModel.Description;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedConfigurationForEndpointOrAddress : INeedAddress
    {
        INeedConfigurationForBindingOrAddress WithEndpointConfiguration(Action<ServiceEndpoint> configurationAction);
    }
}
