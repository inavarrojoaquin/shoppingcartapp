using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.ApplyDiscount
{
    public class DiscountRequest : IBaseRequest
    {
        public string Name { get; }
        public int Quantity { get; }
        public DiscountRequest(DiscountDTO discountDTO)
        {
            if (string.IsNullOrEmpty(discountDTO.DiscountName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "DiscountName"));

            Name = discountDTO.DiscountName;

            if (discountDTO.DiscountQuantity < 0)
                throw new Exception(string.Format("Error: {0} can not be less than 0", "DiscountQuantity"));

            Quantity = discountDTO.DiscountQuantity;
        }
    }
}