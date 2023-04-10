using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetShoppingCartById(ShoppingCartId id);
        void Save(ShoppingCart shoppingCart);
    }
}