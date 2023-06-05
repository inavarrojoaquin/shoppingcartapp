namespace ShoppingCartApp.Shared.Domain
{
    public interface IEventBus
    {
        Task Publish<T>(IReadOnlyCollection<T> domainEvents) where T : IDomainEvent;
        void Subscribe<T>(IEventHandler<T> eventHandler) where T : IDomainEvent;
    }
}