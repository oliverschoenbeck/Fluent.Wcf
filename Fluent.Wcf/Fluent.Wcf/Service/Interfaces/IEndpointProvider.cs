using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Fluent.Wcf.Service.Interfaces
{
    internal interface IEndpointProvider
    {
        Uri GetUri();

        ServiceEndpoint CreateEndpoint(ServiceHost host);
    }
}
