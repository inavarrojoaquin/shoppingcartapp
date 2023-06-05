using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.ApplyDiscount
{
    public class DiscountRequest : IBaseRequest
    {
        public DiscountId Id { get; }
        public Quantity Quantity { get; }
        public ShoppingCartId ShoppingCartId { get; }
        public DiscountRequest(DiscountDTO discountDTO)
        {
            Id = new DiscountId(discountDTO.DiscountId);
            Quantity = new Quantity(discountDTO.DiscountQuantity);
            ShoppingCartId = new ShoppingCartId(discountDTO.ShoppingCartId);
        }
    }
}