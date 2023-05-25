namespace ShoppingCartApp.Shared.Domain
{
    public interface IQueryBus
    {
        Task<TResult> SendAsync<T, TResult>(T query) where T : IQuery<TResult>;
    }
}
