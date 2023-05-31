namespace ShoppingCartApp.Shared.Domain
{
    public interface IEventBus
    {
        void Publish(IReadOnlyCollection<IDomainEvent> domainEvents);
        
        void Execute(IDomainEvent domainEvent);
        void Register(IEventHandler<IDomainEvent> eventHandler);
    }
}