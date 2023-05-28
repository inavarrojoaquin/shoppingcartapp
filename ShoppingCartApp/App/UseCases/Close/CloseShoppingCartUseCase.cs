using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.Close;

public class CloseShoppingCartUseCase: IBaseUseCase<CloseShoppingCartRequest>
{
    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly IEventBus eventBus;

    public CloseShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository, IEventBus eventBus)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.eventBus = eventBus;
    }

    public async Task ExecuteAsync(CloseShoppingCartRequest request)
    {
        var shoppingCart =
            await this.shoppingCartRepository.GetShoppingCartByIdAsync(new ShoppingCartId(request.ShoppingCartId));
        shoppingCart.Close();
        await this.shoppingCartRepository.SaveAsync(shoppingCart);
        var domainEvent = shoppingCart.GetEvents().First(); 
        await eventBus.Publish(domainEvent);
    }
}