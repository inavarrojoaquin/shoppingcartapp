namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class Discount
    {
        private DiscountId id;
        private Name name;
        private Quantity quantity;

        public Discount(DiscountId id, Name name, Quantity quantity)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
        }

        public double GetCalculatedDiscount()
        {
            return Math.Round((double)quantity.Value() / 100, 2);
        }

        public string Print()
        {
            return string.Format("Promotion: {0}% off with code {1}", quantity, name);
        }
    }
}