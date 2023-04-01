using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartAppTest.App.UseCases.DeleteProduct
{
    internal class DeleteProductUseCaseShould
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;
        private IBaseUseCase<DeleteProductRequest> deleteProductUseCase;

        [SetUp]
        public void SetUp()
        {
            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministratorService>();
            shoppingCartAdministrator.DeleteProductFromShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<OrderItem>());

            deleteProductUseCase = new DeleteProductUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void DeleteProductToShoppingCartSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ShoppingCartName = "Test",
            };
            DeleteProductRequest productRequest = new DeleteProductRequest(productDTO);

            Assert.DoesNotThrow(() => deleteProductUseCase.Execute(productRequest));

            shoppingCartAdministrator.Received(1).DeleteProductFromShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<OrderItem>());
        }

        [Test]
        public void RaiseExWhenDeleteProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => deleteProductUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest))));
        }
    }
}
