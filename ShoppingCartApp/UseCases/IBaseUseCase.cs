using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public interface IBaseUseCase<T> where T : IBaseRequest
    {
        void Execute(T request);
    }
}