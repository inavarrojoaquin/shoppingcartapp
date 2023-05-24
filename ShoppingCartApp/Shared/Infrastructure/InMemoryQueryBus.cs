using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure
{
    public class InMemoryQueryBus : IQueryBus
    {
        private readonly IServiceProvider serviceProvider;

        public InMemoryQueryBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handlerType = typeof(IQueryHandler<TQuery, TResult>);
            IQueryHandler<TQuery, TResult>? handler = serviceProvider.GetService(handlerType) as IQueryHandler<TQuery, TResult>;
            if (handler == null) throw new Exception("Error handling query: " + handlerType);
            
            return await handler.Handle(query);
        }
    }
}
