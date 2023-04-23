namespace ShoppingCartApp.App.Domain
{
    public class Discount
    {
        private DiscountId discountId;
        private DiscountName discountName;
        private Quantity discountQuantity;
        
        public Discount(DiscountId id, DiscountName name, Quantity quantity)
        {
            this.discountId = id;
            this.discountName = name;
            this.discountQuantity = quantity;
        }

        public DiscountData ToPrimitives()
        {
            return new DiscountData
            {
                DiscountId = discountId.Value(),
                DiscountName = discountName.Value(),
                DiscountQuantity = discountQuantity.Value()
            };
        }

        public static Discount FromPrimitives(DiscountData discountData)
        {
            return new Discount(new DiscountId(discountData.DiscountId),
                                new DiscountName(discountData.DiscountName),
                                new Quantity(discountData.DiscountQuantity));
        }

        public double CalculateDiscount(double totalPrice)
        {
            double total = totalPrice * (discountQuantity.Value() / 100);
            return Math.Round(total, 2);
        }

        public DiscountId GetDiscountId()
        {
            return discountId;
        }
    }

    public class DiscountData
    {
        public string DiscountId { get; set; }
        public string DiscountName { get; set; }
        public int DiscountQuantity { get; set; }
    }
}