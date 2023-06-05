using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.DeleteProduct
{
    public class DeleteProductRequest : IBaseRequest
    {
        public ProductId ProductId { get; }
        public ShoppingCartId ShoppingCartId { get; }
        public DeleteProductRequest(ProductDTO productDTO)
        {
            ProductId = new ProductId(productDTO.ProductId);
            ShoppingCartId = new ShoppingCartId(productDTO.ShoppingCartId);
        }
    }
}