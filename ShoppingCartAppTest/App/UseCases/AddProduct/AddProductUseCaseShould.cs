using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartAppTest.App.UseCases.AddProduct
{
    internal class AddProductUseCaseShould
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;
        private IBaseUseCase<AddProductRequest> addProductUseCase;

        [SetUp]
        public void SetUp()
        {

            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministratorService>();
            shoppingCartAdministrator.AddProductToShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());

            addProductUseCase = new AddProductUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void AddProductToShoppingCartSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ProductPrice = 1,
                ProductQuantity = 1,
                ShoppingCartName = "Test",
            };
            AddProductRequest productRequest = new AddProductRequest(productDTO);

            Assert.DoesNotThrow(() => addProductUseCase.Execute(productRequest));

            shoppingCartAdministrator.Received(1).AddProductToShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());
        }

        [Test]
        public void RaiseExWhenProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => addProductUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(AddProductRequest))));
        }
    }
}
