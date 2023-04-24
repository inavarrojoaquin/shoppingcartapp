using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private ShoppingCartContext context;

        public ShoppingCartRepository(ShoppingCartContext context)
        {
            this.context = context;
        }

        public ShoppingCart GetShoppingCartById(ShoppingCartId shoppingCartId)
        {
            ShoppingCartData? shoppingCartData = context.ShoppingCart.FirstOrDefault(x => x.ShoppingCartId == shoppingCartId.Value());

            if (shoppingCartData == null)
                return null;

            return ShoppingCart.FromPrimitives(shoppingCartData);
        }

        public void Save(ShoppingCart shoppingCart)
        {
            ShoppingCartData shoppingCartData = shoppingCart.ToPrimitives();
            var state = context.Entry(shoppingCartData).State;
            if (state == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.ShoppingCart.Add(shoppingCartData);
            }
            else
            {
                context.Entry(shoppingCartData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}