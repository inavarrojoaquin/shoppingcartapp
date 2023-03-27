using System.Text;

namespace ShoppingCartApp
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly string name;
        private readonly List<Product> products;

        public ShoppingCart(string name, List<Product> products)
        {
            this.name = name;
            this.products = products;
        }

        public void AddProduct(Product product)
        {
            if (products.Contains(product))
            {
                Product findedProduct = products.First(x => x.Equals(product));
                findedProduct.AddQuantity();
                return;
            }

            products.Add(product);
        }

        public string PrintProducts()
        {
            if (!products.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            foreach (var item in products)
                productList.AppendLine(item.ToString());

            return productList.ToString();
        }

        public string PrintTotalNumberOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
        }

        public double GetTotalPrice()
        {
            return products.Sum(x => x.CalculatePrice());
        }

        public void DeleteProduct(Product product)
        {
            Product findedProduct = products.Find(x => x.Equals(product));
            if (findedProduct.Quantity > 1)
            {
                findedProduct.DecreaseQuantity();
                return;
            }

            products.Remove(product);
        }

        public override bool Equals(object? obj)
        {
            return obj is ShoppingCart cart &&
                   name == cart.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        
        }
        private int GetTotalOfProducts()
        {
            return products.Sum(x => x.Quantity);
        }
    }
}