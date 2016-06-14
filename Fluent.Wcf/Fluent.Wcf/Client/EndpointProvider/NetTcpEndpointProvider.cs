using System.ServiceModel;

namespace Fluent.Wcf.Client.EndpointProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    public class NetTcpEndpointProvider<TInterface> : BaseEndpointProvider<NetTcpBinding, TInterface>
        where TInterface : class
    {
        public NetTcpEndpointProvider(ServiceClientFactory<TInterface> parent) : base(parent) {}

        public override NetTcpBinding CreateBinding()
        {
            var binding = new NetTcpBinding();
            
            if (ConfigurationAction != null)
                ConfigurationAction.Invoke(binding);

            return binding;
        }

        public override EndpointAddress CreateEndpoint()
        {
            return new EndpointAddress(Address);
        }
    }
}
