using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.UseCases
{
    public interface IBaseUseCase<T> where T : IBaseRequest
    {
        Task ExecuteAsync(T request);
    }
}