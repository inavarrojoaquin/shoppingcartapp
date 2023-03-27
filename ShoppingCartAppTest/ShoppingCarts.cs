using System.Text;

namespace ShoppingCartApp
{
    public class ShoppingCarts
    {
        private List<ShoppingCart> shoppingCarts;
        public ShoppingCarts(List<ShoppingCart> shoppingCartList)
        {
            shoppingCarts = shoppingCartList;
        }

        public void AddProductToShoppingCart(ShoppingCart shoppingCart, Product product)
        {
            if (shoppingCarts.Contains(shoppingCart))
            {
                ShoppingCart? findedShoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));
                findedShoppingCart.AddProduct(product);
                return;
            }

            shoppingCart.AddProduct(product);
            shoppingCarts.Add(shoppingCart);
        }

        public void DeleteProductFromShoppingCart(ShoppingCart shoppingCart, Product product)
        {
            if (!shoppingCarts.Contains(shoppingCart))
                throw new Exception("Error ShoppingCart does not exists when deleting product");

            ShoppingCart? findedShoppingCart = shoppingCarts.Find(x => x.Equals(shoppingCart));
            findedShoppingCart.DeleteProduct(product);
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