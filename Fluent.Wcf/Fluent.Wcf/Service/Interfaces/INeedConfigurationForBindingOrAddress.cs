using System;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedConfigurationForBindingOrAddress : INeedAddress
    {
        INeedConfigurationForEndpointOrAddress WithBindingConfiguration(Action<Binding> configurationAction);
    }
}
