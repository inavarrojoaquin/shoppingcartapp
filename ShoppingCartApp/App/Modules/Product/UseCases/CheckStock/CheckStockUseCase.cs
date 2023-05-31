using ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockUseCase : IBaseUseCase<CheckStockRequest>
    {
        //public CheckStockUseCase(IProductRepository productRepository, IEventBus eventBus)
        //{

        //}
        public Task ExecuteAsync(CheckStockRequest request)
        {
            // ir al repo de los productos y restar la cantidad que me han pasado, aunq luego sea negativo y se lanzaria otro evento X
            System.Console.WriteLine("Checking Stock status...");
            
            return Task.CompletedTask;
        }
    }
}
