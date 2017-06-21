using System.ServiceModel;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedEndpoint<TService, TInterface>
        where TService : TInterface
        where TInterface : class
    {
        INeedConfigurationOrAddress<TService, TInterface, NetTcpBinding> UsingNetTcp();

        INeedConfigurationOrAddress<TService, TInterface, BasicHttpBinding> UsingBasicHttp();

        INeedConfigurationOrAddress<TService, TInterface, WebHttpBinding> UsingWebHttp();
    }
}
