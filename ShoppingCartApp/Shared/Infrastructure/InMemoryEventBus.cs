using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Func<IDomainEvent, Task>>> _eventHandlers;

    public InMemoryEventBus()
    {
        _eventHandlers = new Dictionary<Type, List<Func<IDomainEvent, Task>>>();
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        // if (_eventHandlers.TryGetValue(typeof(TEvent), out var handlers))
        if (_eventHandlers.TryGetValue(@event.GetType(), out var handlers))
        {
            foreach (var handler in handlers)
            {
                await handler(@event);
            }
        }
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> eventHandler) where TEvent : IDomainEvent
    {
        if (!_eventHandlers.ContainsKey(typeof(TEvent)))
        {
            _eventHandlers[typeof(TEvent)] = new List<Func<IDomainEvent, Task>>();
        }

        _eventHandlers[typeof(TEvent)].Add((@event) => eventHandler((TEvent)@event));
    }
}