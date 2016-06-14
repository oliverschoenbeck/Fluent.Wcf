using System.ServiceModel;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedCreationOrEndpoint<TService, TInterface> : INeedEndpoint<TService, TInterface>
        where TService : TInterface
        where TInterface : class
    {
        ServiceHost Create();

        ServiceHost CreateNoMeta();
    }
}
