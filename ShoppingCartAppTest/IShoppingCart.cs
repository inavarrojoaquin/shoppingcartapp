﻿namespace ShoppingCartApp
{
    public interface IShoppingCart
    {
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        string PrintTotalNumberOfProducts();
        double GetTotalPrice();
    }
}