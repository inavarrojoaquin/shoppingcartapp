using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.UseCases
{
    public interface IBaseUseCase<T> where T : IBaseRequest
    {
        void Execute(T request);
    }
    
    public interface IBaseUseCase<in T, out TR> where T : IBaseRequest
    {
        TR Execute(T request);
    }
}