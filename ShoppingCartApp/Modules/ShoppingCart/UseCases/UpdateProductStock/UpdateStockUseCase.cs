using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.UpdateProductStock
{
    public class UpdateStockUseCase : IBaseUseCase<UpdatetSockRequest>
    {
        public async Task ExecuteAsync(UpdatetSockRequest request)
        {
            Console.WriteLine("Updaing product Stock..." + request.ProductData.ProductName);

            await Task.CompletedTask;
        }
    }
}
