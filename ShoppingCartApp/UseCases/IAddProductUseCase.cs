using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public interface IAddProductUseCase
    {
        void Execute(AddProductRequest productRequest);
    }
}