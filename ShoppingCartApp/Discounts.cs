using System.Text;

namespace ShoppingCartApp
{
    public class Discounts
    {
        private List<Discount> discounts;

        public Discounts(List<Discount> discountList)
        {
            discounts = discountList;
        }

        public void ApplyDiscount(Discount discount)
        {
            if (discounts.Contains(discount))
                throw new Exception("The discount is already applied");
            
            discounts.Add(discount);
        }

        public double GetDiscount()
        {
            if (!discounts.Any())
                return -1;

            double acumulatedDiscounts = 0;
            discounts.ForEach(x => acumulatedDiscounts += x.GetCalculatedDiscount());
            
            return acumulatedDiscounts;
        }

        public string PrintDiscount()
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