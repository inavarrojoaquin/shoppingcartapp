namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProduct(Product product);
        string PrintShoppingCart();
    }
}