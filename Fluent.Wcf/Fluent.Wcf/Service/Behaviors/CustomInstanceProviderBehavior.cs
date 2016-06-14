using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Fluent.Wcf.Service.Behaviors
{
    /// <summary>
    /// A ServiceBehaviour used to inject a custom IInstanceProvider into the wcf chain.
    /// Using a customized IInstanceProvider enables controll over service instantiation.
    /// </summary>
    public class CustomInstanceProviderBehavior : IServiceBehavior
    {
        private readonly IInstanceProvider _instanceProvider;

        public CustomInstanceProviderBehavior(IInstanceProvider instanceProvider)
        {
            _instanceProvider = instanceProvider;
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {}

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters) {}

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers,
        /// message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var cd = (ChannelDispatcher) channelDispatcherBase;
                
                foreach (var ed in cd.Endpoints)
                {
                    if (!ed.IsSystemEndpoint)
                    {
                        ed.DispatchRuntime.InstanceProvider = _instanceProvider;
                    }
                }
            }
        }
    }
}