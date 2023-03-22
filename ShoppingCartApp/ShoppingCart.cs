namespace ShoppingCartApp
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly string name;
        private readonly Products products;

        public ShoppingCart(string name)
        {
            this.name = name;
            products = new Products();
        }

        public void AddProduct(Product product)
        {
            products.AddProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            products.DeleteProduct(product);
        }

        internal string? PrintProducts()
        {
            return products.PrintProducts();
        }

        internal string? PrintTotalNumberOfProducts()
        {
            return products.PrintTotalNumberOfProducts();
        }

        internal double PrintTotalPice()
        {
            return products.GetTotalPrice();
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
    }
}