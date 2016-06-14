using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Fluent.Wcf.ExampleContext;
using Fluent.Wcf.Service;

namespace Fluent.Wcf.ExampleHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var serviceHost = ServiceFactory
            //    .CreateService(typeof (ExampleService), true)
            //    .UsingInterface(typeof (IExampleService))
            //    .UsingNetTcp().At("net.tcp://0:8090/ExampleService")
            //    .Create();
            //serviceHost.Open();

            var sh =
                ServiceFactory<ExampleService, IExampleService>
                    .CreateService(new ExampleService())
                    .UsingNetTcp().At("net.tcp://0:8090/ExampleService")
                    .BasicHttpTcp().At("http://0:8080/ExampleService")
                    .Create();
            sh.Open();

            Console.WriteLine("Press 'ENTER' to close service.");
            Console.ReadLine();
        }

        public class MyInstanceProvider : IInstanceProvider
        {
            public object GetInstance(InstanceContext instanceContext)
            {
                return new WrongExampleService(2);
            }

            public object GetInstance(InstanceContext instanceContext, Message message)
            {
                return new WrongExampleService(2);
            }

            public void ReleaseInstance(InstanceContext instanceContext, object instance)
            {
                
            }
        }


        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] 
        public class ExampleService : IExampleService
        {
            public int Add(int a, int b)
            {
                return a + b;
            }
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        public class WrongExampleService : IExampleService
        {
            private readonly int _multiplicator;

            //public WrongExampleService()
            //{
                
            //}

            public WrongExampleService(int multiplicator)
            {
                _multiplicator = multiplicator;
            }

            public int Add(int a, int b)
            {
                return a + b * _multiplicator;
            }
        }
    }
}
