using System.Text;

namespace ShoppingCartApp
{
    internal class ShoppingCart : IShoppingCart
    {
        private Products products;
        private Discounts discounts;

        public ShoppingCart()
        {
            products = new Products();
            discounts = new Discounts();
        }

        public void AddProduct(Product product)
        {
            products.AddProduct(product);
        }

        public void ApplyDiscount(Discount discount)
        {
            discounts.ApplyDiscount(discount);
        }

        public void DeleteProduct(Product product)
        {
            products.DeleteProduct(product);
        }

        public string PrintShoppingCart()
        {
            StringBuilder shoppingCartBuilder = new();
            shoppingCartBuilder.AppendLine(products.PrintProducts());
            shoppingCartBuilder.AppendLine(discounts.PrintDiscount());
            shoppingCartBuilder.AppendLine(products.PrintTotalOfProducts());
            shoppingCartBuilder.AppendLine(PrintTotalPrice());

            return shoppingCartBuilder.ToString();
        }

        private string? PrintTotalPrice()
        {
            double totalPrice = products.GetTotalPrice();
            double discount = discounts.GetDiscount();

            if(discount != -1)
                totalPrice -= totalPrice * discount;

            return string.Format("Total price: {0}", Math.Round(totalPrice, 2));
        }
    }
}