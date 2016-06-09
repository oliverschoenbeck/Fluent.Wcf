using System;
using System.ServiceModel.Channels;

namespace Fluent.Wcf.Client.Interfaces
{
    public interface INeedBindingConfigurationOrAddress<out TBinding, TInterface> : INeedAddress<TBinding, TInterface>
        where TBinding : Binding
        where TInterface : class
    {
        INeedAddress<TBinding, TInterface> WithConfiguration(Action<TBinding> configurationAction);
    }
}