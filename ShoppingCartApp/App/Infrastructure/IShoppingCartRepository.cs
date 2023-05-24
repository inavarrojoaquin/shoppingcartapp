using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByIdAsync(ShoppingCartId id);
        Task SaveAsync(ShoppingCart shoppingCart);
    }
}