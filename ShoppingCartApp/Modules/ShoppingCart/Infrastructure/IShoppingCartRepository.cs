using ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByIdAsync(ShoppingCartId id);
        Task SaveAsync(ShoppingCart shoppingCart);
    }
}