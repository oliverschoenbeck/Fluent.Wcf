using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluent.Wcf.ExampleContext;
using Fluent.Wcf.Service;

namespace Fluent.Wcf.ExampleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = ServiceFactory
                .CreateService(typeof (ExampleService), true)
                .UsingInterface(typeof (IExampleService))
                .UsingNetTcp().At("net.tcp://0:8090/ExampleService")
                .Create();
            serviceHost.Open();

            Console.WriteLine("Press 'ENTER' to close service.");
            Console.ReadLine();
        }

        public class ExampleService : IExampleService
        {
            public int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
