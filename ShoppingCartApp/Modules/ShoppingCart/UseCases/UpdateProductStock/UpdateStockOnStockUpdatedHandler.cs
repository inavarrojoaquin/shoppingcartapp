using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.UpdateProductStock
{
    public class UpdateStockOnStockUpdatedHandler : IEventHandler<StockUpdated>
    {
        private readonly IBaseUseCase<UpdatetSockRequest> useCase;

        public UpdateStockOnStockUpdatedHandler(IBaseUseCase<UpdatetSockRequest> useCase)
        {
            this.useCase = useCase;
        }
        public async Task Handle(StockUpdated domainEvent)
        {
            await useCase.ExecuteAsync(new UpdatetSockRequest { ProductData = domainEvent.ProductData });
        }
    }
}
