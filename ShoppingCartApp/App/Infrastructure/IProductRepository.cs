using ShoppingCartApp.App.Domain;

namespace ShoppingCartAppTest.App.UseCases.AddProduct
{
    public interface IProductRepository
    {
        Product GetProductById(ProductId productId);
        void DeleteProductById(ProductId productId);
        void Save(Product product);
    }
}