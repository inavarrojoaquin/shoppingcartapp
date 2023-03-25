using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public interface IApplyDiscountUseCase
    {
        void Execute(DiscountRequest discountRequest);
    }
}