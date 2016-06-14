using System.ServiceModel.Channels;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedConfigurationOrAddress<TService, TInterface, TBinding> :
        INeedConfigurationForEndpointOrAddress<TService, TInterface, TBinding>,
        INeedConfigurationForBindingOrAddress<TService, TInterface, TBinding>
        where TService : TInterface
        where TInterface : class
        where TBinding : Binding
    {
        
    }
}
