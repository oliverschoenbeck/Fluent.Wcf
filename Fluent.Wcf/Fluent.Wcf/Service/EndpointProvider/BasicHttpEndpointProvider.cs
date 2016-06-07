using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Service.EndpointProvider
{
    public class BasicHttpEndpointProvider : BaseEndpointProvider
    {
        internal BasicHttpEndpointProvider(ServiceFactory parent) : base(parent) {}

        public override Binding EndpointBinding
        {
            get
            {
                return new BasicHttpBinding();
            }
        }

        
    }
}