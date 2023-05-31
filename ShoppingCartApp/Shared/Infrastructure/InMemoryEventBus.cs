using ShoppingCartApp.Shared.Domain;
using System.Reflection;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryEventBus : IEventBus
    {
        private IDictionary<Type, object> handlers;

        public InMemoryEventBus()
        {
            handlers = new Dictionary<Type, object>();
        }

        public void Publish(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            throw new NotImplementedException();
        }
        
        public void Execute(IDomainEvent domainEvent)
        {
            ((IEventHandler<IDomainEvent>)handlers[domainEvent.GetType()]).Handle(domainEvent);
        }

        public void Register(IEventHandler<IDomainEvent> eventHandler)
        {
            //get type of IDomainEvent
            //handlers.Add(eventHandler.GetType().GetNestedType("IDomainEvent"), eventHandler);
            //handlers.Add(typeof(T), eventHandler);
        }
    }
}
