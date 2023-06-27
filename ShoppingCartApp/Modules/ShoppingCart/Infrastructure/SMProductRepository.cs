using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;

public class SMProductRepository : ISMProductRepository
{
    private readonly ShoppingCartDbContext context;

    public SMProductRepository(ShoppingCartDbContext context)
    {
        this.context = context;
    }

    public async Task<Product> GetProductByIdAsync(ProductId id)
    {
        if (id == null) return null;

        var productData = await context.Products.FirstOrDefaultAsync((product) => product.ProductId.Equals(id.Value()));

        if (productData == null) return null;
        
        return Product.FromPrimitives(productData);
    }

    public async Task SaveAsync(Product product)
    {
        var productInternalData = product.ToPrimitives();
        if (context.Entry(productInternalData).State == EntityState.Detached)
            context.Add(productInternalData);
        else
            context.Entry(productInternalData).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }
}