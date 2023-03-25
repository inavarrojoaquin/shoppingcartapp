using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public interface IDeleteProductUseCase
    {
        void Execute(DeleteProductRequest productRequest);
    }
}