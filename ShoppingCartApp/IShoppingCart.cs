namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProduct(Product product);
        void ApplyDiscount(Discount discount);
        void DeleteProduct(Product product);
        string PrintShoppingCart();
    }
}