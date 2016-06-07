using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Fluent.Wcf.Service.EndpointProvider;
using Fluent.Wcf.Service.Interfaces;

namespace Fluent.Wcf.Service
{
    public class ServiceFactory : INeedInterface, INeedCreationOrEndpoint
    {
        internal List<IEndpointProvider> EndpointProviders { get; private set; }

        internal Type ServiceType { get; private set; }
        internal Type ServiceInterfaceType { get; private set; }

        bool IsMexEnabled { get; set; }
        bool IsHttpGetEnabled { get; set; }

        private ServiceFactory(Type serviceType)
        {
            ServiceType = serviceType;
            EndpointProviders = new List<IEndpointProvider>();
        }

        public static INeedInterface CreateService(Type serviceType, bool mexEnabled = false, bool httpGetEnabled = false)
        {
            return new ServiceFactory(serviceType)
            {
                IsMexEnabled = mexEnabled,
                IsHttpGetEnabled = httpGetEnabled
            };
        }

        public INeedEndpoint UsingInterface(Type interfacType)
        {
            ServiceInterfaceType = interfacType;
            return this;
        }

        public INeedConfigurationOrAddress UsingNetTcp()
        {
            return new NetTcpEndpointProvider(this);
        }

        public INeedConfigurationOrAddress UsingBasicHttp()
        {
            return new BasicHttpEndpointProvider(this);
        }

        private ServiceHost _serviceHost;
        public ServiceHost Create()
        {
            _serviceHost = new ServiceHost(ServiceType, EndpointProviders.Select(ep => new Uri(ep.EndpointAddress)).ToArray());
            EndpointProviders.ForEach(ep =>
            {
                var endpoint = _serviceHost.AddServiceEndpoint(ServiceInterfaceType, ep.EndpointBinding, "");
                if (ep.HasEndpointConfiguration)
                    ep.EndpointConfigurationAction.Invoke(endpoint);
            });
            EnableMetaDataIfConfigured();
            return _serviceHost;
        }

        private void EnableMetaDataIfConfigured()
        {
            if (!IsMexEnabled && !IsHttpGetEnabled)
                return;

            var meBehavior = _serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() ??
                             new ServiceMetadataBehavior();
            meBehavior.HttpGetEnabled = IsHttpGetEnabled;
            _serviceHost.Description.Behaviors.Add(meBehavior);

            if (!IsMexEnabled)
                _serviceHost.AddServiceEndpoint(
                    ServiceMetadataBehavior.MexContractName,
                    MetadataExchangeBindings.CreateMexHttpBinding(),
                    "mex"
                    );
        }
    }
}
