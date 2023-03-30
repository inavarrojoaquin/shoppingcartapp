using ShoppingCartApp.App.Domain;
using System.Text;

namespace ShoppingCartApp.App.Services.ShoppingCartAdministrator
{
    public class ShoppingCartAdministratorService : IShoppingCartAdministratorService
    {
        private ShoppingCarts shoppingCarts;
        private Discounts discounts;

        public ShoppingCartAdministratorService(ShoppingCarts shoppingCarts, Discounts discounts)
        {
            this.shoppingCarts = shoppingCarts;
            this.discounts = discounts;
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
            double totalPriceWithDiscounts = GetTotalPriceWithDiscounts(shoppingCart);

            StringBuilder shoppingCartAdministratorBuilder = new();
            shoppingCartAdministratorBuilder.AppendLine(shoppingCartResult);
            shoppingCartAdministratorBuilder.AppendLine(discountsResult);
            shoppingCartAdministratorBuilder.AppendLine(string.Format("Total price: {0}", totalPriceWithDiscounts));

            return shoppingCartAdministratorBuilder.ToString();
        }

        public double GetTotalPriceWithDiscounts(ShoppingCart shoppingCart)
        {
            double totalPrice = shoppingCarts.GetTotalPrice(shoppingCart);
            double discount = discounts.GetDiscount();

            if (discount != -1)
                totalPrice -= totalPrice * discount;

            return Math.Round(totalPrice, 2);
        }
    }
}