using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.PrintShoppingCart;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.PrintShoppingCart
{
    internal class PrintShoppingCartUseCaseShould
    {
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<PrintShoppingCartRequest> printShoppingCartUseCase;

        [SetUp]
        public void SetUp()
        {
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            printShoppingCartUseCase = new PrintShoppingCartUseCase(shoppingCartRepository);
        }

        [Test]
        public void PrintShoppingCart()
        {
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(new ShoppingCart(ShoppingCartId.Create()));

            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO
            {
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };
            PrintShoppingCartRequest printShoppingCartRequest = new PrintShoppingCartRequest(shoppingCartDTO);

            printShoppingCartUseCase.Execute(printShoppingCartRequest);

            shoppingCartRepository.Received(1).GetShoppingCartById(Arg.Any<ShoppingCartId>());
        }

        [Test]
        public void RaiseExWhenPrintShoppingCartRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => printShoppingCartUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", nameof(PrintShoppingCartRequest))));
        }
    }
}
