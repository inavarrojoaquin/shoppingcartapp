namespace ShoppingCartApp.Shared.Domain
{
    public interface IQueryBus
    {
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
