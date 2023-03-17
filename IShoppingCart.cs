namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        void AddProducts(List<Product> productsToAdd);
        string PrintShoppingCart();
    }
}