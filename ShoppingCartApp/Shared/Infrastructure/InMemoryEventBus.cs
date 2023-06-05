using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryEventBus : IEventBus
    {
        private IDictionary<Type, List<object>> handlers;

        public InMemoryEventBus()
        {
            handlers = new Dictionary<Type, List<object>>();
        }

        public async Task Publish<T>(IReadOnlyCollection<T> domainEvents) where T : IDomainEvent
        {
            try
            {
                foreach (var domainEvent in domainEvents)
                {
                    var typeofT = typeof(T);
                    List<object> eventHandlers = handlers[typeofT];

                    foreach (var eventHandler in eventHandlers)
                    {
                        IEventHandler<T> concreteEventHandler = (IEventHandler<T>)eventHandler;
                        await concreteEventHandler.Handle(domainEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Subscribe<T>(IEventHandler<T> eventHandler) where T : IDomainEvent
        {
            if(!handlers.ContainsKey(typeof(T)))
                handlers.Add(typeof(T), new List<object>());

            handlers[typeof(T)].Add(eventHandler);
        }
    }
}
