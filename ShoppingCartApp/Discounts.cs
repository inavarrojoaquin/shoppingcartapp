using System.Text;

namespace ShoppingCartApp
{
    internal class Discounts
    {
        private List<Discount> discounts;

        public Discounts()
        {
            discounts = new List<Discount>();
        }

        internal void ApplyDiscount(Discount discount)
        {
            if (discounts.Contains(discount))
                throw new Exception("The discount is already applied");
            
            discounts.Add(discount);
        }

        internal double GetDiscount()
        {
            if (!discounts.Any())
                return -1;

            double acumulatedDiscounts = 0;
            discounts.ForEach(x => acumulatedDiscounts += x.GetCalculatedDiscount());
            
            return acumulatedDiscounts;
        }

        internal string PrintDiscount()
        {
            if (!discounts.Any())
                return "No promotion";

            StringBuilder productList = new();
            foreach (var item in discounts)
                productList.AppendLine(item.ToString());

            return productList.ToString();
        }
    }
}