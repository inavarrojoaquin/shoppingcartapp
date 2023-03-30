using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.UseCases.PrintShoppingCart;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartAppTest.App.UseCases.PrintShoppingCart
{
    internal class PrintShoppingCartUseCaseShould
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;
        private IBaseUseCase<PrintShoppingCartRequest> printShoppingCartUseCase;

        [SetUp]
        public void SetUp()
        {
            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministratorService>();
            shoppingCartAdministrator.PrintShoppingCart(Arg.Any<ShoppingCart>());

            printShoppingCartUseCase = new ShoppingCartUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void PrintShoppingCartSuccessfully()
        {
            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO
            {
                ShoppingCartName = "Test",
            };
            PrintShoppingCartRequest printShoppingCartRequest = new PrintShoppingCartRequest(shoppingCartDTO);

            Assert.DoesNotThrow(() => printShoppingCartUseCase.Execute(printShoppingCartRequest));

            shoppingCartAdministrator.Received(1).PrintShoppingCart(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void RaiseExWhenPrintShoppingCartRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => printShoppingCartUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(PrintShoppingCartRequest))));
        }
    }
}
