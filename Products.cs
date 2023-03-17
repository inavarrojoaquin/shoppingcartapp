using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        List<Product> products;

        public Products()
        {
            products = new List<Product>();
        }

        internal void AddProduct(Product product)
        {
            if (products.Exists(x => x.Name == product.Name))
            {
                Product findedProduct = products.First(x => x.Name == product.Name);
                findedProduct.Quantity++;
                return;
            }

            products.Add(product);
        }

        internal string PrintProducts()
        {
            if (!products.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            products.ForEach(x => productList.AppendLine(string.Format("-> Name: {0} \t| Price: {1} \t| Quantity: {2}", x.Name, x.Price, x.Quantity)));

            return productList.ToString();
        }

        internal string PrintTotalOfProducts()
        {
            return string.Format("Total of products: {0}", products.Sum(x => x.Quantity));
        }

        internal string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", Math.Round(products.Sum(x => x.Price * x.Quantity), 2));
        }
    }
}