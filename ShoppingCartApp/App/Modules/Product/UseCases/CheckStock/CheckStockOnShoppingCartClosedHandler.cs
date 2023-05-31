using ShoppingCartApp.App.Modules.ProductModule.UseCases.CheckStock;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockOnShoppingCartClosedHandler : IEventHandler<ShoppingCartClosed>
    {
        private readonly IBaseUseCase<CheckStockRequest> useCase;

        public CheckStockOnShoppingCartClosedHandler(IBaseUseCase<CheckStockRequest> useCase)
        {
            this.useCase = useCase;
        }
        public async Task Handle(ShoppingCartClosed domainEvent)
        {
            await useCase.ExecuteAsync(new CheckStockRequest { ShoppingCartData = domainEvent.ShoppingCartData });
        }
    }
}
