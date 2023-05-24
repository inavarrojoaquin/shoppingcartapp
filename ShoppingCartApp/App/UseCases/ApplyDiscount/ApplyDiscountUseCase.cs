using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.ApplyDiscount
{
    public class ApplyDiscountUseCase : IBaseUseCase<DiscountRequest>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ApplyDiscountUseCase(IDiscountRepository discountRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.discountRepository = discountRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task ExecuteAsync(DiscountRequest discountRequest)
        {
            if (discountRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DiscountRequest)));

            Discount discount = discountRepository.GetDiscountById(discountRequest.Id);
            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(discountRequest.ShoppingCartId);

            if (discount == null)
                throw new Exception(string.Format("Error: There is no discount for id: {0}", discountRequest.Id.Value()));
            if (shoppingCart == null)
                throw new Exception(string.Format("Error: There is no shoppingCart for id: {0}", discountRequest.ShoppingCartId.Value()));

            shoppingCart.ApplyDiscount(discount);

            await shoppingCartRepository.SaveAsync(shoppingCart);
        }
    }
}