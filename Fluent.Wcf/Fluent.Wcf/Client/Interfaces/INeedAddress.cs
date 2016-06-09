namespace Fluent.Wcf.Client.Interfaces
{
    public interface INeedAddress<out TBinding, TInterface>
    {
        INeedCreation<TInterface> At(string address);
    }
}