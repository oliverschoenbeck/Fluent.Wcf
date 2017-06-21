using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Fluent.Wcf.Service.Interfaces;

namespace Fluent.Wcf.Service.EndpointProvider
{
    /// <summary>
    /// Base class for all EndpointProviders.
    /// </summary>
    public abstract class BaseEndpointProvider<TService, TInterface, TBinding> :
        INeedConfigurationOrAddress<TService, TInterface, TBinding>,
        IEndpointProvider
        where TService : class, TInterface
        where TInterface : class
        where TBinding : Binding
    {
        internal string Address;
        
        internal ServiceFactory<TService, TInterface> Parent;
        
        internal Action<ServiceEndpoint> EndpointConfigurationAction;
        
        internal Action<TBinding> BindingConfigurationAction;

        protected BaseEndpointProvider(ServiceFactory<TService, TInterface> parent)
        {
            Parent = parent;
        }

        public INeedCreationOrEndpoint<TService, TInterface> At(string address)
        {
            Address = address;
            Parent.EndpointProvider.Add(this);
            return Parent;
        }

        public INeedConfigurationForBindingOrAddress<TService, TInterface, TBinding> WithEndpointConfiguration(Action<ServiceEndpoint> configurationAction)
        {
            EndpointConfigurationAction = configurationAction;
            return this;
        }

        public INeedConfigurationForEndpointOrAddress<TService, TInterface, TBinding> WithBindingConfiguration(Action<TBinding> configurationAction)
        {
            BindingConfigurationAction = configurationAction;
            return this;
        }

        public abstract Uri GetUri();

        public abstract ServiceEndpoint CreateEndpoint(ServiceHost serviceHost);
    }
}