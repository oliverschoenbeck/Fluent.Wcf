using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.Wcf.Service.EndpointProvider
{
    public class WebHttpEndpointProvider<TService, TInterface> : BaseEndpointProvider<TService, TInterface, WebHttpBinding>
        where TService : class, TInterface
        where TInterface : class
    {
        public WebHttpEndpointProvider(ServiceFactory<TService, TInterface> parent) : base(parent)
        {
        }

        public override Uri GetUri()
        {
            return new Uri(Address);
        }

        public override ServiceEndpoint CreateEndpoint(ServiceHost serviceHost)
        {
            var binding = new WebHttpBinding();
            BindingConfigurationAction?.Invoke(binding);

            var endpoint = serviceHost.AddServiceEndpoint(typeof(TInterface), binding, "");
            EndpointConfigurationAction?.Invoke(endpoint);

            return endpoint;
        }
    }    
}
