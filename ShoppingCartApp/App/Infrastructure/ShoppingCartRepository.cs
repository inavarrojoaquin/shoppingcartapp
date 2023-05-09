using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ShoppingCartDbContext context;

    public ShoppingCartRepository(ShoppingCartDbContext context)
    {
        this.context = context;
    }
    public ShoppingCart GetShoppingCartById(ShoppingCartId id)
    {
        if (id == null) return null;

        var shoppingCartData = context.ShoppingCarts.FirstOrDefault(sc => sc.ShoppingCartId.Equals(id.Value()));
        
        if (shoppingCartData == null) return null;
        
        return ShoppingCart.FromPrimitives(shoppingCartData);
    }

    public void Save(ShoppingCart shoppingCart)
    {
        var shoppingCartData = shoppingCart.ToPrimitives();
        if (context.Entry(shoppingCartData).State == EntityState.Detached)
            context.Add(shoppingCartData);
        else
            context.Entry(shoppingCartData).State = EntityState.Modified;
        context.SaveChanges();
    }
}