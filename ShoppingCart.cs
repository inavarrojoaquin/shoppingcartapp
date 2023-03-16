namespace ShoppingCartApp
{
    internal class ShoppingCart : IShoppingCart
    {
        private IEnumerable<Product> products;
        private string promotion;

        public ShoppingCart()
        {
            products = new List<Product>();
            promotion = string.Empty;
        }

        public List<Product> GetProducts()
        {
            return products.ToList();
        }

        public string GetPromotion()
        {
            return promotion;
        }

        public int GetTotalOfProducts()
        {
            return products.Count();
        }

        public float GetTotalPrice()
        {
            return products.Sum(x => x.Price );
        }
    }
}