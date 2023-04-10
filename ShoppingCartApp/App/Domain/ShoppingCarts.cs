using System.Text;

namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCarts
    {
        private List<ShoppingCart> shoppingCarts;
        public ShoppingCarts(List<ShoppingCart> shoppingCartList)
        {
            shoppingCarts = shoppingCartList;
        }

        public void AddProductToShoppingCart(ShoppingCart shoppingCart, OrderItem orderItem)
        {
            //if (shoppingCarts.Contains(shoppingCart))
            //{
            //    ShoppingCart? findedShoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));
            //    findedShoppingCart.AddProduct(orderItem);
            //    return;
            //}

            //shoppingCart.AddProduct(orderItem);
            //shoppingCarts.Add(shoppingCart);
        }

        public void DeleteProductFromShoppingCart(ShoppingCart shoppingCart, OrderItem orderItem)
        {
            //if (!shoppingCarts.Contains(shoppingCart))
            //    throw new Exception("Error ShoppingCart does not exists when deleting product");

            //ShoppingCart? findedShoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));
            //findedShoppingCart.DeleteProduct(orderItem);
        }

        public double GetTotalPrice(ShoppingCart shoppingCart)
        {
            if (shoppingCarts.Contains(shoppingCart))
                shoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));

            return shoppingCart.GetTotalPrice();
        }

        public string PrintShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCarts.Contains(shoppingCart))
                shoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));

            StringBuilder shoppingCartBuilder = new();
            shoppingCartBuilder.AppendLine(shoppingCart.PrintProducts());
            shoppingCartBuilder.AppendLine(shoppingCart.PrintTotalNumberOfProducts());
            
            return shoppingCartBuilder.ToString();
        }
    }
}