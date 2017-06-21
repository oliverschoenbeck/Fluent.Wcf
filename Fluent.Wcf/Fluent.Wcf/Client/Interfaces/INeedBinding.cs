using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Client.Interfaces
{
    public interface INeedBinding<TInterface>
        where TInterface : class
    {
        INeedBindingConfigurationOrAddress<NetTcpBinding, TInterface> UsingNetTcp();

        INeedBindingConfigurationOrAddress<BasicHttpBinding, TInterface> UsingBasicHttp();

        INeedBindingConfigurationOrAddress<WebHttpBinding, TInterface> UsingWebHttp();
    }
}
