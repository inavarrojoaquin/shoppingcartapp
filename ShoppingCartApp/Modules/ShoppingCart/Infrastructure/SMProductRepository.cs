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

    public Product GetProductById(ProductId id)
    {
        if (id == null) return null;

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