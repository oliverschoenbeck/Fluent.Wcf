using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Fluent.Wcf.Client.EndpointProvider;
using Fluent.Wcf.Client.Interfaces;

namespace Fluent.Wcf.Client
{
    /// <summary>
    /// The ServiceClientFactory is used to create a ServicClient using a fluent interface.
    /// </summary>
    /// <typeparam name="TInterface">The Service Interface</typeparam>
    public class ServiceClientFactory<TInterface> : INeedBinding<TInterface>, INeedCreation<TInterface>
        where TInterface : class
    {
        /// <summary>
        /// private ctor
        /// </summary>
        private ServiceClientFactory() {}
        
        /// <summary>
        /// Start creating a new ServiceChannel implementing TInterface
        /// </summary>
        public static INeedBinding<TInterface> CreateClient()
        {
            return new ServiceClientFactory<TInterface>();
        }
        
        /// <summary>
        /// The EndpointProvider choosen.
        /// </summary>
        internal IEndpointProvider<Binding> EndpointProvider;

        /// <summary>
        /// Creates a NetTcpBinding for the ServiceClient.
        /// </summary>
        public INeedBindingConfigurationOrAddress<Binding, TInterface> UsingNetTcp()
        {
            return new NetTcpEndpointProvider<TInterface>(this);
        }

        /// <summary>
        /// Creates a BasicHttpBinding for the ServiceClient.
        /// </summary>
        public INeedBindingConfigurationOrAddress<Binding, TInterface> UsingBasicHttp()
        {
            return new BasicHttpEndpointProvider<TInterface>(this);
        }

        /// <summary>
        /// Returns the ServiceClient.
        /// </summary>
        public ServiceClient<TInterface> Create()
        {
            return new ServiceClient<TInterface>(ChannelFactory<TInterface>.CreateChannel(
                EndpointProvider.CreateBinding(),
                EndpointProvider.CreateEndpoint()
                ));
        }
    }
}