using ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure
{
    public interface IProductRepository
    {
        Product GetProductById(ProductId id);
        void Save(Product product);
    }
}