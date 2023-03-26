﻿namespace ShoppingCartApp
{
    public interface IShoppingCartAdministrator
    {
        void AddProductToShoppingCart(ShoppingCart shoppingCart, Product product);
        void DeleteProductFromShoppingCart(ShoppingCart shoppingCart, Product product);
        void ApplyDiscount(Discount discount);
        string PrintShoppingCart(ShoppingCart shoppingCart);
        double GetTotalPriceWithDiscounts(ShoppingCart shoppingCart);
    }
}