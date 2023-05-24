namespace ShoppingCartApp.Shared.Domain
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
