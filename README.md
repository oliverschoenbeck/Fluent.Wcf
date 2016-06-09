# Fluent.Wcf
A fluent interface to WCF.

### Service example

```c#

var serviceHost = ServiceFactory
			.CreateService(typeof (ExampleService), true)
			.UsingInterface(typeof (IExampleService))
			.UsingNetTcp()
			.At("net.tcp://0:8090/ExampleService")
			.Create();
		serviceHost.Open();
		
````

### Client example
```c#

var client = ServiceClientFactory<IExampleService>
                .CreateClient()
                .UsingNetTcp()
                .At("net.tcp://127.0.0.1:8090/ExampleService")
                .Create();
		
````