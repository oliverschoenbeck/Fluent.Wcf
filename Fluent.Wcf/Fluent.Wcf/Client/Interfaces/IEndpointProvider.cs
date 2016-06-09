using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Client.Interfaces
{
    public interface IEndpointProvider<out TBinding> where TBinding : Binding
    {
        TBinding CreateBinding();

        EndpointAddress CreateEndpoint();
    }
}