using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.DeleteProduct
{
    public class DeleteProductUseCase : IBaseUseCase<DeleteProductRequest>
    {
        private readonly ISMProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public DeleteProductUseCase(ISMProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task ExecuteAsync(DeleteProductRequest deleteRequest)
        {
            if (deleteRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest)));

            Product product = await productRepository.GetProductByIdAsync(deleteRequest.ProductId);
            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(deleteRequest.ShoppingCartId);

            if (shoppingCart == null)
                throw new Exception(string.Format("Error: There is no shoppingCart for id: {0}", deleteRequest.ShoppingCartId));

            shoppingCart.DeleteProduct(product);

            await shoppingCartRepository.SaveAsync(shoppingCart);
        }
    }
}