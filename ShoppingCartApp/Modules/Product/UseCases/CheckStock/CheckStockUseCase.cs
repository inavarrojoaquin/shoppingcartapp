using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockUseCase : IBaseUseCase<CheckStockRequest>
    {
        //public CheckStockUseCase(IProductRepository productRepository, IEventBus eventBus)
        //{

        //}
        public Task ExecuteAsync(CheckStockRequest request)
        {
            // ir al repo de los productos y restar la cantidad que me han pasado, aunq luego sea negativo y se lanzaria otro evento X
            Console.WriteLine("Checking Stock status...");

            return Task.CompletedTask;
        }
    }
}
