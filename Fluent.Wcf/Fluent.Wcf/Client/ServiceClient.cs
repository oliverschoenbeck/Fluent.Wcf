using System.ServiceModel.Channels;

namespace Fluent.Wcf.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceClient<TInterface>
    {
        private readonly TInterface _channel;

        /// <summary>
        /// Creates a new ServiceClient.
        /// </summary>
        /// <param name="channel"></param>
        public ServiceClient(TInterface channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// The Interface to the WCF Service
        /// </summary>
        public TInterface Service
        {
            get { return _channel; }
        }

        /// <summary>
        /// The Interface to the IChannel Interface of the connection
        /// </summary>
        public IChannel Channel
        {
            get
            {
                return (IChannel)_channel;
            }
        }
    }
}
