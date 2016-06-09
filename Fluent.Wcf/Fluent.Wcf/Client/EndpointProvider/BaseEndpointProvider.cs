using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Fluent.Wcf.Client.Interfaces;

namespace Fluent.Wcf.Client.EndpointProvider
{
    public abstract class BaseEndpointProvider<TBinding, TInterface> : INeedBindingConfigurationOrAddress<TBinding, TInterface>, IEndpointProvider<TBinding>
        where TBinding : Binding
        where TInterface : class
    {
        internal Action<TBinding> ConfigurationAction; 
        internal string Address;
        internal ServiceClientFactory<TInterface> Parent;

        internal BaseEndpointProvider(ServiceClientFactory<TInterface> parent)
        {
            Parent = parent;
        }

        public INeedAddress<TBinding, TInterface> WithConfiguration(Action<TBinding> configurationAction)
        {
            ConfigurationAction = configurationAction;
            return this;
        }

        public INeedCreation<TInterface> At(string address)
        {
            Address = address;
            Parent.EndpointProvider = this;
            return Parent;
        }

        public abstract TBinding CreateBinding();
        public abstract EndpointAddress CreateEndpoint();
    }
}