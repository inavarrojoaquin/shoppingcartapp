using NSubstitute;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
using PRODUCT_DOMAIN = ShoppingCartApp.Modules.ProductModule.Domain;
using SHOPPINGCART_DOMAIN = ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartAppTest.Modules.ShoppingCartModule.Infrastructure
{
    internal class CloseShoppingCartShould
    {
        [Test]
        public void CloseShoppingCartRunningCloseEvent()
        {
            IShoppingCartRepository shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            string shoppingCartIdValue = Guid.NewGuid().ToString();
            SHOPPINGCART_DOMAIN.ShoppingCartData shoppingCartData = new SHOPPINGCART_DOMAIN.ShoppingCartData
            {
                ShoppingCartId = shoppingCartIdValue,
                ShoppingCartName = SHOPPINGCART_DOMAIN.ShoppingCartName.Create().Value(),
                OrderItems = new List<SHOPPINGCART_DOMAIN.OrderItemData> 
                { 
                    new SHOPPINGCART_DOMAIN.OrderItemData 
                    { 
                        OrderItemId = SHOPPINGCART_DOMAIN.OrderItemId.Create().Value(),
                        ProductId = SHOPPINGCART_DOMAIN.ProductId.Create().Value()
                    } 
                }
            };
            
            shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Any<SHOPPINGCART_DOMAIN.ShoppingCartId>())
                .Returns(SHOPPINGCART_DOMAIN.ShoppingCart.FromPrimitives(shoppingCartData));
            IEventBus eventBus = new InMemoryEventBus();
            ShoppingCartApp.Modules.ProductModule.Infrastructure.IProductRepository productRepository = Substitute.For<ShoppingCartApp.Modules.ProductModule.Infrastructure.IProductRepository>();

            PRODUCT_DOMAIN.Product product = new PRODUCT_DOMAIN.Product(PRODUCT_DOMAIN.ProductId.Create(), PRODUCT_DOMAIN.Name.Create(), PRODUCT_DOMAIN.ProductPrice.Create(), PRODUCT_DOMAIN.ProductStock.Create());
            productRepository.GetProductById(Arg.Any<PRODUCT_DOMAIN.ProductId>()).Returns(product);

            IBaseUseCase<CheckStockRequest> checkStockUseCase = new CheckStockUseCase(productRepository, eventBus);
            IEventHandler<ShoppingCartClosed> shoppingCartClosedEventHandler = new CheckStockOnShoppingCartClosedHandler(checkStockUseCase);
            IEventHandler<ProductStockUpdated> productUpdatedEventHandler = null;

            eventBus.Subscribe(shoppingCartClosedEventHandler);
            eventBus.Subscribe(productUpdatedEventHandler);

            IBaseUseCase<CloseShoppingCartRequest> useCase = new CloseShoppingCartUseCase(shoppingCartRepository, eventBus);

            CloseShoppingCartRequest request = new CloseShoppingCartRequest(new ShoppingCartDTO { ShoppingCartId = shoppingCartIdValue });
            useCase.ExecuteAsync(request);

            productRepository.Received(1).SaveAsync(Arg.Is<PRODUCT_DOMAIN.Product>(x => x.ToPrimitives().ProductId == product.ToPrimitives().ProductId));
        }
    }
}
