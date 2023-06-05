using NSubstitute;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.CloseShoppingCart
{
    internal class CloseShoppingCartShould
    {
        [Test]
        public void CloseShoppingCartRunningCloseEvent()
        {
            IShoppingCartRepository shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            IEventBus eventBus = new InMemoryEventBus();
            IBaseUseCase<CheckStockRequest> checkStockUseCase = new CheckStockUseCase();
            IEventHandler<ShoppingCartClosed> eventHandler = new CheckStockOnShoppingCartClosedHandler(checkStockUseCase);

            eventBus.Subscribe(eventHandler);

            IBaseUseCase<CloseShoppingCartRequest> useCase = new CloseShoppingCartUseCase(shoppingCartRepository, eventBus);

            CloseShoppingCartRequest request = new CloseShoppingCartRequest(new ShoppingCartDTO { ShoppingCartId = Guid.NewGuid().ToString() });
            useCase.ExecuteAsync(request);
        }
    }
}
