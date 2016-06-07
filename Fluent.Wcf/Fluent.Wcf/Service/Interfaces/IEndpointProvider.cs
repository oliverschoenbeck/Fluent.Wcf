using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Fluent.Wcf.Service.Interfaces
{
    internal interface IEndpointProvider
    {
        string EndpointAddress { get; }

        bool HasEndpointConfiguration { get; }

        Action<ServiceEndpoint> EndpointConfigurationAction { get; }

        Binding EndpointBinding { get; }

        Action<Binding> BindingConfigurationAction { get; }
    }
}
