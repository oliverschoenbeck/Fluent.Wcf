using System;

namespace Fluent.Wcf.Client
{
    public class ServiceClientFactory
    {
        private Type _interfaceType;
        private ServiceClientFactory(Type interfaceType)
        {
            _interfaceType = interfaceType;
        }
        public static ServiceClientFactory CreateClient<T>()
        {
            return new ServiceClientFactory(typeof(T));
        }




    }
}
