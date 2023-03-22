using System.Text;

namespace ShoppingCartApp
{
    internal class Products
    {
        private List<Product> products;
        
        public Products()
        {
            products = new List<Product>();
        }

        internal void AddProduct(Product product)
        {
            if (products.Contains(product)) {
                Product findedProduct = products.First(x => x.Equals(product));
                findedProduct.AddQuantity();
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
            foreach (var item in products) 
                productList.AppendLine(item.ToString());
            
            return productList.ToString();
        }

        internal string PrintTotalNumberOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
        }

        private int GetTotalOfProducts()
        {
            return products.Sum(x => x.Quantity);
        }

        public double GetTotalPrice()
        {
            return products.Sum(x => x.CalculatePrice()); 
        }

        internal void DeleteProduct(Product product)
        {
            if (products.Contains(product))
            {
                Product findedProduct = products.First(x => x.Equals(product));
                findedProduct.DecreaseQuantity();
                return;
            }

            products.Remove(product);
        }
    }
}