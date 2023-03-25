using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public class ApplyDiscountUseCase : IApplyDiscountUseCase
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;

        public ApplyDiscountUseCase(IShoppingCartAdministrator shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(DiscountRequest discountRequest)
        {
            if (discountRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DiscountRequest)));

            Discount discount = new Discount(discountRequest.Name, discountRequest.Quantity);

            shoppingCartAdministrator.ApplyDiscount(discount);
        }
    }
}