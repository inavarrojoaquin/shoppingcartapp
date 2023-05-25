using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.UseCases
{
    /// <summary>
    /// Used for CQRS Commands 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseUseCase<T> where T : IBaseRequest
    {
        Task ExecuteAsync(T request);
    }

    /// <summary>
    /// Used for CQRS Queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IBaseUseCase<T, TResult> where T : IBaseRequest
    {
        Task<TResult> ExecuteAsync(T request);
    }
}