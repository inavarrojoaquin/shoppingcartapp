using NSubstitute;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.AddProduct
{
    internal class AddProductUseCaseShould
    {
        private ISMProductRepository productRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<AddProductRequest> addProductUseCase;

        [SetUp]
        public void SetUp()
        {
            productRepository = Substitute.For<ISMProductRepository>();
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            addProductUseCase = new AddProductUseCase(productRepository, shoppingCartRepository);
        }

        [Test]
        public async Task AddProductToNewShoppingCartSuccessfully()
        {
            ProductId productId = ProductId.Create();
            ShoppingCart nullShoppingCart = null;
            productRepository.GetProductByIdAsync(Arg.Any<ProductId>()).Returns(new Product(ProductId.Create(), Name.Create(), ProductPrice.Create(), ProductStock.Create()));
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>()).Returns(x => (ShoppingCart)null);

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = productId.Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };

            await addProductUseCase.ExecuteAsync(new AddProductRequest(productDTO));

            productRepository.Received(1).GetProductByIdAsync(Arg.Any<ProductId>());
            shoppingCartRepository.Received(1).GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).SaveAsync(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void AddProductToExistingShoppingCartSuccessfully()
        {
            ProductId productId = ProductId.Create();
            productRepository.GetProductByIdAsync(Arg.Any<ProductId>()).Returns(new Product(ProductId.Create(), Name.Create(), ProductPrice.Create(), ProductStock.Create()));
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>()).Returns(new ShoppingCart(ShoppingCartId.Create()));

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = productId.Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };

            addProductUseCase.ExecuteAsync(new AddProductRequest(productDTO));

            productRepository.Received(1).GetProductByIdAsync(Arg.Any<ProductId>());
            shoppingCartRepository.Received(1).GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).SaveAsync(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void RaiseExWhenProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => addProductUseCase.ExecuteAsync(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(AddProductRequest))));
        }
    }
}
