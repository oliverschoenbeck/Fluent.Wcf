namespace Fluent.Wcf.Service.Interfaces
{
    public interface INeedAddress<TService, TInterface, TBinding>
        where TService : TInterface
        where TInterface : class
    {
        INeedCreationOrEndpoint<TService, TInterface> At(string address);
    }
}
