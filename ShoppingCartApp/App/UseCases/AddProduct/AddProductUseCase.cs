using Microsoft.Extensions.Logging;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartApp.App.UseCases.AddProduct
{
    public class AddProductUseCase : IBaseUseCase<AddProductRequest>
    {
        private IProductRepository productRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private readonly ILogger<AddProductUseCase> logger;

        public AddProductUseCase(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, ILogger<AddProductUseCase> logger)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.logger = logger;
        }

        public void Execute(AddProductRequest productRequest)
        {
            logger.LogInformation("Executing AddProduct use case");
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(AddProductRequest)));

            Product product = productRepository.GetProductById(productRequest.ProductId);
            if (product == null) throw new Exception("Product not found");
            ShoppingCart shoppingCart = shoppingCartRepository.GetShoppingCartById(productRequest.ShoppingCartId);
            
            if(shoppingCart == null)
                shoppingCart = new ShoppingCart(productRequest.ShoppingCartId);
            
            shoppingCart.AddProduct(product);

            shoppingCartRepository.Save(shoppingCart);
            logger.LogInformation("Executed AddProduct use case");
        }
    }
}