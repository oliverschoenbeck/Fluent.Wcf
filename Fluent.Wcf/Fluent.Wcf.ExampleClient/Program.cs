using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Fluent.Wcf.Client;
using Fluent.Wcf.ExampleContext;

namespace Fluent.Wcf.ExampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8080/ExampleService");
            var service = ChannelFactory<IExampleService>.CreateChannel(binding, endpoint);

            Console.WriteLine(service.Add(1,1));
            Console.ReadLine();

            ServiceClientFactory
                .CreateClient<IExampleService>()
                .UsingNetTcp()
                .WithConfiguration((b) => {})
                .At("http://localhost:8080/ExampleService")
                .Create();
        }
    }
}
