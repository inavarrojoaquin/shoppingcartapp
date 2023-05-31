namespace ShoppingCartApp.Shared.Domain
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
