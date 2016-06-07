using System.ServiceModel;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedCreationOrEndpoint : INeedEndpoint
    {
        ServiceHost Create();
    }
}
