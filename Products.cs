using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        private List<Product> products;
        private double discount;
        private string promotion;

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
            promotion = promo;
            
            if (promotion == "PROMO_5")
                discount = 5;
            if (promotion == "PROMO_10")
                discount = 10;
            if (promotion == "PROMO_15")
                discount = 15;
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

            return string.Format("Promotion: {0}% off with code {1}", discount, promotion);
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