using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.Close;

public class CloseShoppingCartUseCase: IBaseUseCase<CloseShoppingCartRequest>
{
    private readonly IShoppingCartRepository shoppingCartRepository;

    public CloseShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository)
    {
        this.shoppingCartRepository = shoppingCartRepository;
    }

    public async Task ExecuteAsync(CloseShoppingCartRequest request)
    {
        var shoppingCart =
            await this.shoppingCartRepository.GetShoppingCartByIdAsync(new ShoppingCartId(request.ShoppingCartId));
        shoppingCart.Close();
        await this.shoppingCartRepository.SaveAsync(shoppingCart);
    }
}