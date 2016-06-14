using System;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedConfigurationForBindingOrAddress<TService, TInterface, TBinding> : INeedAddress<TService, TInterface, TBinding>
        where TService : TInterface
        where TInterface : class
        where TBinding : Binding
    {
        INeedConfigurationForEndpointOrAddress<TService, TInterface, TBinding> WithBindingConfiguration(Action<TBinding> configurationAction);
    }
}
