using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.UseCases
{
    public interface IBaseUseCase<T> where T : IBaseRequest
    {
        Task ExecuteAsync(T request);
    }

    public interface IBaseUseCase<T, TResult> where T : IBaseRequest
    {
        Task<TResult> ExecuteAsync(T request);
    }
}