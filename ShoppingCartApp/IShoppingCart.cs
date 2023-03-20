namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProducts(Product product);
        void ApplyDiscount(string discount);
        void DeleteProduct(string productName);
        string PrintShoppingCart();
    }
}