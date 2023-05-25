namespace ShoppingCartApp.Shared.Domain
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
