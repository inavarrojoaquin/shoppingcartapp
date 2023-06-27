
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CheckProductStock
{
    public class CheckStockUseCase : IBaseUseCase<CheckStockRequest>
    {
        //private readonly IPMProductRepository productRepository;
        private readonly ISMProductRepository productRepository;
        private readonly IEventBus eventBus;

        public CheckStockUseCase(ISMProductRepository productRepository, IEventBus eventBus)
        {
            this.productRepository = productRepository;
            this.eventBus = eventBus;
        }
        public async Task ExecuteAsync(CheckStockRequest request)
        {
            // ir al repo de los productos y restar la cantidad que me han pasado, aunq luego sea negativo y se lanzaria otro evento X
            Console.WriteLine("Checking Stock status...");

            foreach(var item in request.ShoppingCartData.OrderItems)
            {
                Product product = await productRepository.GetProductByIdAsync(new ProductId(item.ProductId));
                product.UpdateStock(item.Quantity);

                //await productRepository.SaveAsync(product);

                //Crear el handler, request y usecase relacionado a este updatestock
                //se pueden lanzar eventos pero si no hay nadie escuchando no pasa nada
                // hacer un handeler para captar el evento pero solo con un consolelog para saber que entro

                await eventBus.Publish<StockUpdated>(product.GetEvents());
            }


            await Task.CompletedTask;
        }
    }
}
