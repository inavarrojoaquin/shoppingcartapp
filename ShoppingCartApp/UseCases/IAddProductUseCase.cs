using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    internal interface IAddProductUseCase
    {
        void Execute(ProductRequest productRequest);
    }
}