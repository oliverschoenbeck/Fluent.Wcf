using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Service.EndpointProvider
{
    public class NetTcpEndpointProvider : BaseEndpointProvider
    {
        internal NetTcpEndpointProvider(ServiceFactory parent) : base(parent) {}

        public override Binding EndpointBinding
        {
            get
            {
                return new NetTcpBinding();
            }
        }
    }
}
