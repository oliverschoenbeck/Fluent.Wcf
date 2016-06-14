using System;
using Fluent.Wcf.Client;
using Fluent.Wcf.ExampleContext;

namespace Fluent.Wcf.ExampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ServiceClientFactory<IExampleService>
                .CreateClient()
                .UsingNetTcp()
                .At("net.tcp://127.0.0.1:8090/ExampleService")
                .Create();

            client.Channel.Opened += (_, __) =>
            {
                Console.WriteLine("Opened!");
                Console.Write("1 + 1 = ");
                Console.WriteLine(client.Service.Add(1, 1));

                Console.Write("Closing .. ");
                client.Channel.Close();
            };
            client.Channel.Closed += (_, __) =>
            {
                Console.WriteLine("Closed!");
            };

            Console.Write("Opening .. ");
            client.Channel.Open();

            Console.ReadLine();
        }
    }
}
