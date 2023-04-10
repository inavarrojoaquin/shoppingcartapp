using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure
{
    public interface IDiscountRepository
    {
        Discount GetDiscountById(DiscountId id);
    }
}