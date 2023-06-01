using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;
        private IDictionary<Type, List<object>> handlers;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            handlers = new Dictionary<Type, List<object>>();
        }

        public async Task Publish_Sin_Subscribe(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var eventType = domainEvent.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                var eventHandlers = serviceProvider.GetServices(handlerType);
                await PublishEvent(domainEvent, eventHandlers);
            }
        }

        private async Task PublishEvent(IDomainEvent domainEvent, IEnumerable<object> eventHandlers)
        {
            foreach (var eventHandler in eventHandlers)
            {
                dynamic concreteEventHandler = eventHandler;
                dynamic concreteEvent = domainEvent;
                await concreteEventHandler.Handle(concreteEvent);
            }
        }

        
        public async Task Publish(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            try
            {
                foreach (var domainEvent in domainEvents)
                {
                    List<object> eventHandlers = handlers[domainEvent.GetType()];
                    await PublishEvent(domainEvent, eventHandlers);
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
