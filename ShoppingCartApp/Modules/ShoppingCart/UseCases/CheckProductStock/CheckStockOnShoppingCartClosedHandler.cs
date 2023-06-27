using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CheckProductStock
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
            await useCase.ExecuteAsync(new CheckStockRequest(domainEvent.ShoppingCartData));
        }
    }
}
