using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Fluent.Wcf.Service.EndpointProvider
{
    /// <summary>
    /// Provides an ServiceEndpoint bound using NetTcpBinding.
    /// </summary>
    public class NetTcpEndpointProvider<TService, TInterface> : BaseEndpointProvider<TService, TInterface, NetTcpBinding>
        where TService : class, TInterface
        where TInterface : class
    {
        internal NetTcpEndpointProvider(ServiceFactory<TService, TInterface> parent) : base(parent) { }

        public override Uri GetUri()
        {
            return new Uri(Address);
        }

        public override ServiceEndpoint CreateEndpoint(ServiceHost serviceHost)
        {
            var binding = new NetTcpBinding();
            if (BindingConfigurationAction != null)
                BindingConfigurationAction.Invoke(binding);

            var endpoint = serviceHost.AddServiceEndpoint(typeof(TInterface), binding, "");
            if (EndpointConfigurationAction != null)
                EndpointConfigurationAction.Invoke(endpoint);
            return endpoint;
        }
    }
}
