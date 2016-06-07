using System;

namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedInterface
    {
        INeedEndpoint UsingInterface(Type interfacType);
    }
}
