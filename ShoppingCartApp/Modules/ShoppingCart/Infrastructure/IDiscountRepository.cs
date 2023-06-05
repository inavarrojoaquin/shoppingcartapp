using ShoppingCartApp.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure
{
    public interface IDiscountRepository
    {
        Discount GetDiscountById(DiscountId id);
    }
}