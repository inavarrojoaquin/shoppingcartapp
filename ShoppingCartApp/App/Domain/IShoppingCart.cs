namespace ShoppingCartApp.App.Domain
{
    public interface IShoppingCart
    {
        void AddProduct(OrderItem orderItem);
        void DeleteProduct(OrderItem orderItem);
        string PrintTotalNumberOfProducts();
        double GetTotalPrice();
    }
}