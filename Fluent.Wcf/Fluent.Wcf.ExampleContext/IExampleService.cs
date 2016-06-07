using System.ServiceModel;

namespace Fluent.Wcf.ExampleContext
{
    [ServiceContract()]
    public interface IExampleService
    {
        [OperationContract]
        int Add(int a, int b);
    }
}
