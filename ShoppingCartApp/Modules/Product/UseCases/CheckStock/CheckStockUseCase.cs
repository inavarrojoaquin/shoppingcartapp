using ShoppingCartApp.Modules.ProductModule.Domain;
using ShoppingCartApp.Modules.ProductModule.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockUseCase : IBaseUseCase<CheckStockRequest>
    {
        private readonly IPMProductRepository productRepository;
        private readonly IEventBus eventBus;

        public CheckStockUseCase(IPMProductRepository productRepository, IEventBus eventBus)
        {
            this.productRepository = productRepository;
            this.eventBus = eventBus;
        }
        public async Task ExecuteAsync(CheckStockRequest request)
        {
            // ir al repo de los productos y restar la cantidad que me han pasado, aunq luego sea negativo y se lanzaria otro evento X
            Console.WriteLine("Checking Stock status...");

            //request.ShoppingCartData.OrderItems.ForEach(async item => 
            //{
            //    //Product product = productRepository.GetProductById(new ProductId(item.ProductId));
            //    //product.UpdateStock(item.Quantity);
                
            //    //await productRepository.SaveAsync(product);
            //    // Crear el handler, request y usecase relacionado a este updatestock 
            //    // se pueden lanzar eventos pero si no hay nadie escuchando no pasa nada
            //    // hacer un handeler para captar el evento pero solo con un consolelog para saber que entro

            //    //await eventBus.Publish<ProductStockUpdated>(product.GetEvents());
            //});
            
            await Task.CompletedTask;
        }
    }
}
