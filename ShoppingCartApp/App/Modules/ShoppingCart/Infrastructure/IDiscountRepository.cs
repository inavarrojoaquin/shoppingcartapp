using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure
{
    public interface IDiscountRepository
    {
        Discount GetDiscountById(DiscountId id);
    }
}