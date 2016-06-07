namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedEndpoint
    {
        INeedConfigurationOrAddress UsingNetTcp();

        INeedConfigurationOrAddress UsingBasicHttp();
    }
}
