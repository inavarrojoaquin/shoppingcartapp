using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ShoppingCartDbContext context;

    public ShoppingCartRepository(ShoppingCartDbContext context)
    {
        this.context = context;
    }
    public async Task<ShoppingCart> GetShoppingCartByIdAsync(ShoppingCartId id)
    {
        if (id == null) return null;

        ShoppingCartData shoppingCartData = context.ShoppingCarts
                                                .Include(orderItem => orderItem.OrderItems)
                                                .FirstOrDefault(sc => sc.ShoppingCartId.Equals(id.Value()));

        if (shoppingCartData == null) return null;

        return ShoppingCart.FromPrimitives(shoppingCartData);
    }

    public async Task SaveAsync(ShoppingCart shoppingCart)
    {
        var shoppingCartData = shoppingCart.ToPrimitives();
        if (context.Entry(shoppingCartData).State == EntityState.Detached)
            context.Add(shoppingCartData);
        else
            context.Entry(shoppingCartData).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }
}