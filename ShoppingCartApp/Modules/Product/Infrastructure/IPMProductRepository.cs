using ShoppingCartApp.Modules.ProductModule.Domain;

namespace ShoppingCartApp.Modules.ProductModule.Infrastructure
{
    public interface IPMProductRepository
    {
        Product GetProductById(ProductId id);
        Task SaveAsync(Product product);
    }
}