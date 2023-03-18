namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProducts(List<Product> productsToAdd);
        void ApplyDiscount(string v);
        string PrintShoppingCart();
    }
}