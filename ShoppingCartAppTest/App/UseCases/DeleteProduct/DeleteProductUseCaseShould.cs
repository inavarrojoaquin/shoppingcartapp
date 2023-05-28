using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartAppTest.App.UseCases.DeleteProduct
{
    internal class DeleteProductUseCaseShould
    {
        private IProductRepository productRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<DeleteProductRequest> deleteProductUseCase;

        [SetUp]
        public void SetUp()
        {
            productRepository = Substitute.For<IProductRepository>();
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            deleteProductUseCase = new DeleteProductUseCase(productRepository, shoppingCartRepository);
        }

        [Test]
        public void DeleteProductFromShoppingCartSuccessfully()
        {
            Product product = new Product(ProductId.Create(), Name.Create(), ProductPrice.Create());
            productRepository.GetProductById(Arg.Any<ProductId>()).Returns(product);
            ShoppingCartId shoppingCartId = ShoppingCartId.Create();
            ShoppingCart shoppingCart = Substitute.For<ShoppingCart>(shoppingCartId);
            shoppingCart.AddProduct(product);
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>()).Returns(shoppingCart);

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = product.GetProductId().Value(),
                ShoppingCartId = shoppingCartId.Value(),
            };

            deleteProductUseCase.ExecuteAsync(new DeleteProductRequest(productDTO));

            productRepository.Received(1).GetProductById(Arg.Any<ProductId>());
            shoppingCartRepository.Received(1).GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).SaveAsync(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void RaiseExWhenDeleteProductRequestIsNull()
        {
            var ex = Assert.ThrowsAsync<Exception>(() => deleteProductUseCase.ExecuteAsync(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest))));
        }

        [Test]
        public void RaiseExWhenProductIdDoesNotExistsInShoppingCart()
        {
            ProductId productId = ProductId.Create();
            productRepository.GetProductById(Arg.Any<ProductId>()).Returns(new Product(productId, Name.Create(), ProductPrice.Create()));
            ShoppingCartId shoppingCartId = ShoppingCartId.Create();
            ShoppingCart shoppingCart = Substitute.For<ShoppingCart>(shoppingCartId);
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>()).Returns(shoppingCart);

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = productId.Value(),
                ShoppingCartId = shoppingCartId.Value(),
            };

            var ex = Assert.ThrowsAsync<Exception>(() => deleteProductUseCase.ExecuteAsync(new DeleteProductRequest(productDTO)));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: The product with id: {0} does not exists in shoppingCart with id: {1}",
                                                  productDTO.ProductId,
                                                  productDTO.ShoppingCartId)));
        }
    }
}
