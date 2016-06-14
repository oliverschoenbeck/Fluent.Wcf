using System.ServiceModel.Channels;

namespace Fluent.Wcf.Client.Interfaces
{
    public interface INeedBinding<TInterface>
        where TInterface : class
    {
        INeedBindingConfigurationOrAddress<Binding, TInterface> UsingNetTcp();

        INeedBindingConfigurationOrAddress<Binding, TInterface> UsingBasicHttp();
    }
}
