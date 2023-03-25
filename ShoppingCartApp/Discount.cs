namespace ShoppingCartApp
{
    public class Discount
    {
        private string name;
        private double quantity;
        
        public Discount(string name, double quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }

        internal double GetCalculatedDiscount()
        {
            return quantity / 100;
        }

        public override string ToString()
        {
            return string.Format("Promotion: {0}% off with code {1}", quantity, name);
        }

        public override bool Equals(object? obj)
        {
            return obj is Discount discount &&
                   name == discount.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }
    }
}