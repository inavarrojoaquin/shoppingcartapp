using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;

namespace ShoppingCartApp.App.Modules.Product.UseCases;

public class CheckStockUseCase : IEventHandler
{
    public async Task Handle(CloseShoppingCartEvent @event)
    {
        Console.WriteLine("Close Shopping Cart event received");
        await Task.CompletedTask;
    }
}