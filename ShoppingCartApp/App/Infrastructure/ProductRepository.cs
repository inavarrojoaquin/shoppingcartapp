using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.App.Domain;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartApp.App.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ShoppingCartDbContext context;

    public ProductRepository(ShoppingCartDbContext context)
    {
        this.context = context;
    }

    public Product GetProductById(ProductId id)
    {
        var productData = context.Products.FirstOrDefault((product) => product.ProductId.Equals(id.Value()));
        if (productData == null) return null;
        return Product.FromPrimitives(productData);
    }

    public void Save(Product product)
    {
        var productInternalData = product.ToPrimitives();
        var state = context.Entry(productInternalData).State;
        if (state == EntityState.Detached)
            context.Add(productInternalData);
        else
            context.Entry(productInternalData).State = EntityState.Modified;

        context.SaveChanges();
    }
}