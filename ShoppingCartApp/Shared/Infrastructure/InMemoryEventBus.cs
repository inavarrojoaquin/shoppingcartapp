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

        public async Task Publish(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            //    var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            //    ICommandHandler<T>? handler = (ICommandHandler<T>)serviceProvider.GetService(handlerType);
            //    if (handler != null)
            //        await handler.Handle(command);

            try
            {
                foreach (var domainEvent in domainEvents)
                {
                    List<object> eventHandlers = handlers[domainEvent.GetType()];

                    foreach (var eventHandler in eventHandlers)
                    {
                        IEventHandler<IDomainEvent> concreteEventHandler = (IEventHandler<IDomainEvent>)eventHandler;
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

        //public async Task SendAsync<T>(T command) where T : ICommand
        //{
        //    var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        //    ICommandHandler<T>? handler = (ICommandHandler<T>)serviceProvider.GetService(handlerType);
        //    if (handler != null)
        //        await handler.Handle(command);
        //}
    }
}
