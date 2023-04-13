namespace ShoppingCartApp.App.Domain
{
    public class Discount
    {
        private DiscountId id;
        private DiscountName name;
        private Quantity quantity;
        
        public Discount(DiscountId id, DiscountName name, Quantity quantity)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
        }

        public DiscountData ToPrimitives()
        {
            return new DiscountData
            {
                Id = id,
                Name = name,
                Quantity = quantity
            };
        }

        public static Discount FromPrimitives(DiscountData discountData)
        {
            return new Discount(discountData.Id,
                                discountData.Name,
                                discountData.Quantity);
        }

        public double CalculateDiscount(double totalPrice)
        {
            double total = totalPrice * (quantity.Value() / 100);
            return Math.Round(total, 2);
        }

    }

    public class DiscountData
    {
        public DiscountId Id { get; set; }
        public DiscountName Name { get; set; }
        public Quantity Quantity { get; set; }
    }
}