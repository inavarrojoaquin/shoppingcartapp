namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProduct(Product product);
        void ApplyDiscount(string discount);
        void DeleteProduct(Product product);
        string PrintShoppingCart();
    }
}