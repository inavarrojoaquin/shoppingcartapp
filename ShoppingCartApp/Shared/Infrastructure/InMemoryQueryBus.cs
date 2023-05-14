using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Infrastructure;

public class InMemoryQueryBus : IQueryBus
{
    private readonly IServiceProvider serviceProvider;

    public InMemoryQueryBus(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;
        if (handler == null)
        {
            throw new InvalidOperationException($"No query handler found for {typeof(TQuery).Name}.");
        }

        return await handler.HandleAsync(query);
    }
}