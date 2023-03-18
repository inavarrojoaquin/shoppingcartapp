namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProducts(List<Product> productsToAdd);
        void ApplyDiscount(string v);
        void DeleteProduct(string productName);
        string PrintShoppingCart();
    }
}