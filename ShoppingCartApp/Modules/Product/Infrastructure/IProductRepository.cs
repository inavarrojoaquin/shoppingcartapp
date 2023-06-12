using ShoppingCartApp.Modules.ProductModule.Domain;

namespace ShoppingCartApp.Modules.ProductModule.Infrastructure
{
    public interface IProductRepository
    {
        Product GetProductById(ProductId id);
        Task SaveAsync(Product product);
    }
}