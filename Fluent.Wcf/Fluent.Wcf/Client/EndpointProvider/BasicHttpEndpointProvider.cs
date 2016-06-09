using System.ServiceModel;

namespace Fluent.Wcf.Client.EndpointProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    public class BasicHttpEndpointProvider<TInterface> : BaseEndpointProvider<BasicHttpBinding, TInterface>
        where TInterface : class
    {
        public BasicHttpEndpointProvider(ServiceClientFactory<TInterface> parent) : base(parent) {}

        public override BasicHttpBinding CreateBinding()
        {
            var binding = new BasicHttpBinding();
            ConfigurationAction.Invoke(binding);
            return binding;
        }

        public override EndpointAddress CreateEndpoint()
        {
            return new EndpointAddress(Address);
        }
    }
}