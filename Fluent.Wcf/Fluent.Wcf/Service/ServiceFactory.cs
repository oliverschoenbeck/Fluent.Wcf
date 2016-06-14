using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Fluent.Wcf.Service.Behaviors;
using Fluent.Wcf.Service.EndpointProvider;
using Fluent.Wcf.Service.Interfaces;

namespace Fluent.Wcf.Service
{
    /// <summary>
    /// ServiceFactory. Creates ServiceHosts in a fluent way.
    /// </summary>
    public class ServiceFactory<TService, TInterface> : INeedCreationOrEndpoint<TService, TInterface>
        where TService : class, TInterface
        where TInterface : class
    {
        /// <summary>
        /// CTOR
        /// </summary>
        private ServiceFactory()
        {
            EndpointProvider = new List<IEndpointProvider>();
        }

        /// <summary>
        /// Holds the singleton instance for the service if set.
        /// </summary>
        internal TService SingletonInstance;

        /// <summary>
        /// A list of endpoint providers that were configured while using the fluent interface.
        /// </summary>
        internal List<IEndpointProvider> EndpointProvider;

        /// <summary>
        /// Can hold a special IInstanceProvider e.g. for DI Scenarios.
        /// If this is set, special endpoint behaviours will be added to 
        /// inject this IInstanceProvider into the WCF Workflow.
        /// </summary>
        internal IInstanceProvider ServiceInstanceProvider;

        /// <summary>
        /// Defines if MEX should be enabled or not.
        /// </summary>
        internal bool MetadataEnabled;

        /// <summary>
        /// Start creating a new ServiceHost
        /// </summary>
        public static INeedEndpoint<TService, TInterface> CreateService()
        {
            return new ServiceFactory<TService, TInterface>();
        }

        /// <summary>
        /// Start creating a new ServiceHost using the singleton service instance given.
        /// Keep in mind that your service needs to be in InstanceContextMode.Single
        /// </summary>
        public static INeedEndpoint<TService, TInterface> CreateService(TService serviceInstance)
        {
            return new ServiceFactory<TService, TInterface>();
        }

        /// <summary>
        /// Start creating a new ServiceHost using the given InstanceProvider to create service instances.
        /// Keep in mind that this will not work for services having InstanceContextMode.Single
        /// </summary>
        public static INeedEndpoint<TService, TInterface> CreateService(IInstanceProvider serviceInstanceProvider)
        {
            return new ServiceFactory<TService, TInterface>
            {
                ServiceInstanceProvider = serviceInstanceProvider
            };
        }

        /// <summary>
        /// Set a singleton instace of TService to use.
        /// </summary>
        public INeedEndpoint<TService, TInterface> AsSingelton(TService singletonInstance)
        {
            SingletonInstance = singletonInstance;
            return this;
        }

        /// <summary>
        /// Add a NetTcp endpoint to your service.
        /// </summary>
        public INeedConfigurationOrAddress<TService, TInterface, NetTcpBinding> UsingNetTcp()
        {
            return new NetTcpEndpointProvider<TService, TInterface>(this);
        }

        /// <summary>
        /// Add a BasicHttp endpoint to your service.
        /// </summary>
        public INeedConfigurationOrAddress<TService, TInterface, BasicHttpBinding> BasicHttpTcp()
        {
            return new BasicHttpEndpointProvider<TService, TInterface>(this);
        }

        /// <summary>
        /// Create the <see cref="ServiceHost"/> instance.
        /// </summary>
        public ServiceHost Create()
        {
            MetadataEnabled = true;
            return InnerCreate();
        }

        public ServiceHost CreateNoMeta()
        {
            MetadataEnabled = false;
            return InnerCreate();
        }

        private ServiceHost InnerCreate()
        {
            var addresses = EndpointProvider.Select(epp => epp.GetUri()).ToArray();
            var serviceHost = SingletonInstance != null
                ? new ServiceHost(SingletonInstance, addresses)
                : new ServiceHost(typeof(TService), addresses);

            EndpointProvider.ForEach(epp =>
            {
                var ep = epp.CreateEndpoint(serviceHost);
            });

            // Add custom service instance provider if given and valid.
            if (ServiceInstanceProvider != null && SingletonInstance == null)
                serviceHost.Description.Behaviors.Add(new CustomInstanceProviderBehavior(ServiceInstanceProvider));

            // Enable metadata exchange.
            if (MetadataEnabled)
            {
                var meBehavior = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() ?? new ServiceMetadataBehavior();
                meBehavior.HttpGetEnabled = EndpointProvider.OfType<BasicHttpEndpointProvider<TService, TInterface>>().Any();
                serviceHost.Description.Behaviors.Add(meBehavior);

                if (EndpointProvider.OfType<BasicHttpEndpointProvider<TService, TInterface>>().Any())
                    serviceHost.AddServiceEndpoint(
                        ServiceMetadataBehavior.MexContractName,
                        MetadataExchangeBindings.CreateMexHttpBinding(),
                        "mex"
                    );
            }

            return serviceHost;
        }
    }
}