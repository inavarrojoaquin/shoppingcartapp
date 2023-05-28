namespace ShoppingCartApp.Shared.Domain;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    void Subscribe<TEvent>(Func<TEvent, Task> eventHandler) where TEvent : IDomainEvent;
}