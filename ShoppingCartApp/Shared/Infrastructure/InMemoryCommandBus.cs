using ShoppingCartApp.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceProvider serviceProvider;

        public InMemoryCommandBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            ICommandHandler<TCommand>? handler = (ICommandHandler<TCommand>)serviceProvider.GetService(handlerType);
            if (handler != null)
                await handler.Handle(command);
        }
    }
}
