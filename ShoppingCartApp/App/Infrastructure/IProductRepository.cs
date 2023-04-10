using ShoppingCartApp.App.Domain;

namespace ShoppingCartAppTest.App.UseCases.AddProduct
{
    public interface IProductRepository
    {
        Product GetProductById(ProductId id);
        void Save();
    }
}