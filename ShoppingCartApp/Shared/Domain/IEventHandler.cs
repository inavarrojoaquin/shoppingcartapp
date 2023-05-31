namespace ShoppingCartApp.Shared.Domain
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
