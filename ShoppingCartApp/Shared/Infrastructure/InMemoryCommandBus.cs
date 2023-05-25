using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceProvider serviceProvider;

        public InMemoryCommandBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            ICommandHandler<T>? handler = (ICommandHandler<T>)serviceProvider.GetService(handlerType);
            if (handler != null)
                await handler.Handle(command);
        }
    }
}
