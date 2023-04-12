using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartApp.App.UseCases.DeleteProduct
{
    public class DeleteProductUseCase : IBaseUseCase<DeleteProductRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public DeleteProductUseCase(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void Execute(DeleteProductRequest deleteRequest)
        {
            ValidateRequest(deleteRequest);
            Product product = LoadProductOrFail(deleteRequest);
            ShoppingCart shoppingCart = LoadShoppingCartOrFail(deleteRequest);

            shoppingCart.DeleteProduct(product);

            shoppingCartRepository.Save(shoppingCart);
        }

        private Product LoadProductOrFail(DeleteProductRequest deleteRequest)
        {
            return productRepository.GetProductById(deleteRequest.ProductId);
        }

        private static void ValidateRequest(DeleteProductRequest deleteRequest)
        {
            if (deleteRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest)));
        }

        private ShoppingCart LoadShoppingCartOrFail(DeleteProductRequest deleteRequest)
        {
            ShoppingCart shoppingCart = shoppingCartRepository.GetShoppingCartById(deleteRequest.ShoppingCartId);

            if (shoppingCart == null)
                throw new Exception(string.Format("Error: There is no shoppingCart for id: {0}", deleteRequest.ShoppingCartId));
            
            return shoppingCart;
        }
    }
}