using ShoppingCartApp.App.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.ApplyDiscount
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