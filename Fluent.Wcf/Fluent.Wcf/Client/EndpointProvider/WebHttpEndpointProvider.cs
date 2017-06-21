using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.Wcf.Client.EndpointProvider
{
    public class WebHttpEndpointProvider<TInterface> : BaseEndpointProvider<WebHttpBinding, TInterface>
        where TInterface : class
    {
        public WebHttpEndpointProvider(ServiceClientFactory<TInterface> parent) : base(parent) {}

        public override WebHttpBinding CreateBinding()
        {
            var binding = new WebHttpBinding();
            ConfigurationAction.Invoke(binding);
            return binding;
        }

        public override EndpointAddress CreateEndpoint()
        {
            return new EndpointAddress(Address);
        }
    }
}
