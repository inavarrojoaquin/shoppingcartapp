using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByIdAsync(ShoppingCartId id);
        Task SaveAsync(ShoppingCart shoppingCart);
    }
}