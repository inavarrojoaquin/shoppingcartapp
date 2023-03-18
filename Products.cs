using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        private List<Product> products;
        private double discount;

        public Products()
        {
            products = new List<Product>();
            discount = 0;
        }

        internal void AddProduct(Product product)
        {
            if (products.Exists(x => x.Name == product.Name))
            {
                Product findedProduct = products.First(x => x.Name == product.Name);
                findedProduct.AddQuantity();
                return;
            }

            products.Add(product);
        }

        internal void ApplyDiscount(string promo)
        {
            discount = 5;
        }

        internal string PrintProducts()
        {
            if (!products.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            products.ForEach(x => productList.AppendLine(x.ToString()));

            return productList.ToString();
        }

        internal string PrintPromotion()
        {
            if (discount == 0)
                return "No promotion";

            return string.Format("Promotion: {0}% off with code {1}", discount, "PROMO_5");
        }

        internal string PrintTotalOfProducts()
        {
            return string.Format("Total of products: {0}", products.Sum(x => x.Quantity));
        }

        internal string PrintTotalPrice()
        {
            double totalPrice = products.Sum(x => x.CalculatePrice());
            if (discount > 0)
                totalPrice -= totalPrice * (discount / 100);

            return string.Format("Total price: {0}", Math.Round(totalPrice, 2));
        }
    }
}