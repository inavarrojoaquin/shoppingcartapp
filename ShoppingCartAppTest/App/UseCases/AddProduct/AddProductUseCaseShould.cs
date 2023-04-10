using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.AddProduct
{
    internal class AddProductUseCaseShould
    {
        private IProductRepository productRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<AddProductRequest> addProductUseCase;

        [SetUp]
        public void SetUp()
        {
            productRepository = Substitute.For<IProductRepository>();
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            addProductUseCase = new AddProductUseCase(productRepository, shoppingCartRepository);
        }

        [Test]
        public void AddProductToNewShoppingCartSuccessfully()
        {
            ProductId productId = ProductId.Create();
            productRepository.GetProductById(Arg.Any<ProductId>()).Returns(new Product(ProductId.Create(), Name.Create(), ProductPrice.Create()));
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(x => null);

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = productId.Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };

            addProductUseCase.Execute(new AddProductRequest(productDTO));

            productRepository.Received(1).GetProductById(Arg.Any<ProductId>());
            shoppingCartRepository.Received(1).GetShoppingCartById(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).Save(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void AddProductToExistingShoppingCartSuccessfully()
        {
            ProductId productId = ProductId.Create();
            productRepository.GetProductById(Arg.Any<ProductId>()).Returns(new Product(ProductId.Create(), Name.Create(), ProductPrice.Create()));
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(new ShoppingCart(ShoppingCartId.Create()));

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = productId.Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };

            addProductUseCase.Execute(new AddProductRequest(productDTO));

            productRepository.Received(1).GetProductById(Arg.Any<ProductId>());
            shoppingCartRepository.Received(1).GetShoppingCartById(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).Save(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void RaiseExWhenProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => addProductUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(AddProductRequest))));
        }
    }
}
