using System.Text;

namespace ShoppingCartApp
{
    internal class ShoppingCart : IShoppingCart
    {
        private Products products;

        public ShoppingCart()
        {
            products = new Products();
        }

        public void AddProducts(List<Product> productsToAdd)
        {
            List<Product> clonesProductsToAdd = productsToAdd.ConvertAll(x => x.Clone());
            clonesProductsToAdd.ForEach(x => products.AddProduct(x));
        }

        public string PrintShoppingCart()
        {
            StringBuilder shoppingCartBuilder = new();
            shoppingCartBuilder.AppendLine(products.PrintProducts());
            shoppingCartBuilder.AppendLine("No promotion");
            shoppingCartBuilder.AppendLine(products.PrintTotalOfProducts());
            shoppingCartBuilder.AppendLine(products.PrintTotalPrice());

            return shoppingCartBuilder.ToString();
        }
    }
}