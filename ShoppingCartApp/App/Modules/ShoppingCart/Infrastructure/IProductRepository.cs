using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure
{
    public interface IProductRepository
    {
        Product GetProductById(ProductId id);
        void Save(Product product);
    }
}