using System.Text;

namespace ShoppingCartApp
{
    internal class ShoppingCart : IShoppingCart
    {
        private List<Product> products;

        public ShoppingCart()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        private string PrintProducts()
        {
            if (!products.Any())            
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            products.ForEach(x => productList.AppendLine(string.Format( "-> Name: {0} \t| Price: {1} \t| Quantity: {2}", x.Name, x.Price, x.Quantity )));

            return productList.ToString();
        }

        private string PrintTotalOfProducts()
        {
            return string.Format("Total of products: {0}", products.Count());
        }

        private string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", products.Sum(x => x.Price));
        }

        public string PrintShoppingCart()
        {
            StringBuilder shoppingCartBuilder = new();
            shoppingCartBuilder.AppendLine(PrintProducts());
            shoppingCartBuilder.AppendLine("No promotion");
            shoppingCartBuilder.AppendLine(PrintTotalOfProducts());
            shoppingCartBuilder.AppendLine(PrintTotalPrice());

            return shoppingCartBuilder.ToString();
        }
    }
}