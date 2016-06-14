using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedConfigurationForEndpointOrAddress<TService, TInterface, TBinding> : INeedAddress<TService, TInterface, TBinding>
        where TService : TInterface
        where TInterface : class
        where TBinding : Binding
    {
        INeedConfigurationForBindingOrAddress<TService, TInterface, TBinding> WithEndpointConfiguration(Action<ServiceEndpoint> configurationAction);
    }
}
