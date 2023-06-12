using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Modules.ProductModule.Domain;

namespace ShoppingCartApp.Modules.ProductModule.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext context;

    public ProductRepository(ProductDbContext context)
    {
        this.context = context;
    }

    public Product GetProductById(ProductId id)
    {
        if (id == null) return null;

        var productData = context.Products.FirstOrDefault((product) => product.ProductId.Equals(id.Value()));

        if (productData == null) return null;

        return Product.FromPrimitives(productData);
    }

    public async Task SaveAsync(Product product)
    {
        var productInternalData = product.ToPrimitives();
        var state = context.Entry(productInternalData).State;
        if (state == EntityState.Detached)
            context.Add(productInternalData);
        else
            context.Entry(productInternalData).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }
}