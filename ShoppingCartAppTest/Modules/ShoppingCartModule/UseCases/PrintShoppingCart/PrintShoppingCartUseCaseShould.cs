using NSubstitute;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.PrintShoppingCart
{
    internal class PrintShoppingCartUseCaseShould
    {
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<PrintShoppingCartRequest, string> printShoppingCartUseCase;

        [SetUp]
        public void SetUp()
        {
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            printShoppingCartUseCase = new PrintShoppingCartUseCase(shoppingCartRepository);
        }

        [Test]
        public void PrintShoppingCart()
        {
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>()).Returns(new ShoppingCart(ShoppingCartId.Create()));

            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO
            {
                ShoppingCartId = ShoppingCartId.Create().Value(),
            };
            PrintShoppingCartRequest printShoppingCartRequest = new PrintShoppingCartRequest(shoppingCartDTO);

            printShoppingCartUseCase.ExecuteAsync(printShoppingCartRequest);

            shoppingCartRepository.Received(1).GetShoppingCartByIdAsync(Arg.Any<ShoppingCartId>());
        }

        [Test]
        public void RaiseExWhenPrintShoppingCartRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => printShoppingCartUseCase.ExecuteAsync(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", nameof(PrintShoppingCartRequest))));
        }
    }
}
