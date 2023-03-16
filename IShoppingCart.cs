namespace ShoppingCartApp
{
    internal interface IShoppingCart
    {
        List<Product> GetProducts();
        string GetPromotion();
        int GetTotalOfProducts();
        float GetTotalPrice();
    }
}