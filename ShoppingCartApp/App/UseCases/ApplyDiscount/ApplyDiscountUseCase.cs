using ShoppingCartApp.App.Domain;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartApp.App.UseCases.ApplyDiscount
{
    public class ApplyDiscountUseCase : IBaseUseCase<DiscountRequest>
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;

        public ApplyDiscountUseCase(IShoppingCartAdministratorService shoppingCartAdministrator)
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