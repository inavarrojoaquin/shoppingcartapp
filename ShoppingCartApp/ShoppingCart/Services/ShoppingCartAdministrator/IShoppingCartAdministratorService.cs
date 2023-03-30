using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Services.ShoppingCartAdministrator
{
    public interface IShoppingCartAdministratorService
    {
        void AddProductToShoppingCart(ShoppingCart shoppingCart, Product product);
        void DeleteProductFromShoppingCart(ShoppingCart shoppingCart, Product product);
        void ApplyDiscount(Discount discount);
        string PrintShoppingCart(ShoppingCart shoppingCart);
        double GetTotalPriceWithDiscounts(ShoppingCart shoppingCart);
    }
}