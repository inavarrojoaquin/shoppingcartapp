using ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure
{
    public interface ISMProductRepository
    {
        Task<Product> GetProductByIdAsync(ProductId id);
        Task SaveAsync(Product product);
    }
}