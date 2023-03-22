using System.Text;

namespace ShoppingCartApp
{
    internal class ShoppingCartAdministrator : IShoppingCartAdministrator
    {
        private ShoppingCarts shoppingCarts;
        private Discounts discounts;

        public ShoppingCartAdministrator()
        {
            shoppingCarts = new ShoppingCarts();
            discounts = new Discounts();
        }

        public void AddProductToShoppingCart(ShoppingCart shoppingCart, Product product)
        {
            shoppingCarts.AddProductToShoppingCart(shoppingCart, product);
        }

        public void DeleteProductFromShoppingCart(ShoppingCart shoppingCart, Product product)
        {
            shoppingCarts.DeleteProductFromShoppingCart(shoppingCart, product);
        }

        public void ApplyDiscount(Discount discount)
        {
            discounts.ApplyDiscount(discount);
        }

        public string PrintShoppingCart(ShoppingCart shoppingCart)
        {
            string shoppingCartResult = shoppingCarts.PrintShoppingCart(shoppingCart);
            string discountsResult = discounts.PrintDiscount();
            string? totalPrice = PrintShoppingCartTotalPrice(shoppingCart);

            StringBuilder shoppingCartAdministratorBuilder = new();
            shoppingCartAdministratorBuilder.AppendLine(shoppingCartResult);
            shoppingCartAdministratorBuilder.AppendLine(discountsResult);
            shoppingCartAdministratorBuilder.AppendLine(totalPrice);

            return shoppingCartAdministratorBuilder.ToString();
        }

        private string? PrintShoppingCartTotalPrice(ShoppingCart shoppingCart)
        {
            double totalPrice = shoppingCarts.GetTotalPrice(shoppingCart);
            double discount = discounts.GetDiscount();

            if (discount != -1)
                totalPrice -= totalPrice * discount;

            return string.Format("Total price: {0}", Math.Round(totalPrice, 2));
        }
    }
}