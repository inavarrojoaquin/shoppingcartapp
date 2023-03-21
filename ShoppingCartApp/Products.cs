using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        private List<Product> products;
        private Discount discount;

        public Products()
        {
            products = new List<Product>();
            discount = new Discount();
        }

        internal void AddProduct(Product product)
        {
            if (products.Contains(product)) {
                Product findedProduct = products.First(x => x.Equals(product));
                findedProduct.AddQuantity();
                return;
            }

            products.Add(product);
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
                productList.AppendLine(item.ToString());
            
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
            return products.Sum(x => x.Quantity);
        }

        internal string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", GetTotalPrice());
        }

        private double GetTotalPrice()
        {
            double totalPrice = products.Sum(x => x.CalculatePrice());
            if (discount.AppliedDiscount > 0)
                totalPrice -= totalPrice * (discount.AppliedDiscount / 100);
            
            return Math.Round(totalPrice, 2);
        }

        internal void DeleteProduct(Product product)
        {
            if (products.Contains(product))
            {
                Product findedProduct = products.First(x => x.Equals(product));
                findedProduct.DecreaseQuantity();
                return;
            }

            products.Remove(product);
        }
    }
}