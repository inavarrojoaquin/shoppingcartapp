namespace ShoppingCartApp.Shared.Domain
{
    public interface IQueryHandler<T, TResult> where T : IQuery<TResult>
    {
        Task<TResult> Handle(T query);
    }
}
