using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;

namespace ShoppingCartAppTest.App.Infrastructure
{
    public class DiscountRepository : IDiscountRepository
    {
        private ShoppingCartDbContext context;

        public DiscountRepository(ShoppingCartDbContext context)
        {
            this.context = context;
        }

        public Discount GetDiscountById(DiscountId id)
        {
            throw new NotImplementedException();
        }
    }
}