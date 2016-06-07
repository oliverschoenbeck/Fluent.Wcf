using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Fluent.Wcf.Service.Interfaces;

namespace Fluent.Wcf.Service.EndpointProvider
{
    public abstract class BaseEndpointProvider : INeedConfigurationOrAddress, IEndpointProvider
    {
        public string EndpointAddress { get; private set; }
        public bool HasEndpointConfiguration { get; private set; }
        public Action<ServiceEndpoint> EndpointConfigurationAction { get; private set; }
        public abstract Binding EndpointBinding { get; }
        public Action<Binding> BindingConfigurationAction { get; private set; }
        internal ServiceFactory Parent { get; private set; }

        public INeedCreationOrEndpoint At(string address)
        {
            EndpointAddress = address;
            Parent.EndpointProviders.Add(this);
            return Parent;
        }

        public INeedConfigurationForEndpointOrAddress WithBindingConfiguration(Action<Binding> configurationAction)
        {
            BindingConfigurationAction = configurationAction;
            return this;
        }

        public INeedConfigurationForBindingOrAddress WithEndpointConfiguration(Action<ServiceEndpoint> configurationAction)
        {
            EndpointConfigurationAction = configurationAction;
            return this;
        }

        internal BaseEndpointProvider(ServiceFactory parent)
        {
            Parent = parent;
        }
    }
}