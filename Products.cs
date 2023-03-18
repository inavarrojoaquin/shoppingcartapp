using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        private Dictionary<string, Product> products;
        private Discount discount;

        public Products()
        {
            products = new Dictionary<string, Product>();
            discount = new Discount();
        }

        internal void AddProduct(Product product)
        {
            if (products.ContainsKey(product.Name))
            {
                products[product.Name].AddQuantity();
                return;
            }

            products.Add(product.Name, product);
        }

        internal void ApplyDiscount(string promotion)
        {
            discount.ApplyPromotion(promotion);
        }

        internal string PrintProducts()
        {
            if (!products.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            foreach (var item in products) 
                productList.AppendLine(item.Value.ToString());
            
            return productList.ToString();
        }

        internal string PrintPromotion()
        {
            if (discount.AppliedDiscount == 0)
                return "No promotion";

            return string.Format("Promotion: {0}% off with code {1}", discount.AppliedDiscount, discount.AppliedPromotion);
        }

        internal string PrintTotalOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
        }

        private int GetTotalOfProducts()
        {
            return products.Sum(x => x.Value.Quantity);
        }

        internal string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", GetTotalPrice());
        }

        private double GetTotalPrice()
        {
            double totalPrice = products.Sum(x => x.Value.CalculatePrice());
            if (discount.AppliedDiscount > 0)
                totalPrice -= totalPrice * (discount.AppliedDiscount / 100);
            
            return Math.Round(totalPrice, 2);
        }
    }
}