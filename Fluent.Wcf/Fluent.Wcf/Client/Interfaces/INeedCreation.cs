namespace Fluent.Wcf.Client.Interfaces
{
    public interface INeedCreation<TInterface>
    {
        ServiceClient<TInterface> Create();
    }
}