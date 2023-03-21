namespace ShoppingCartApp
{
    public class Product
    {
        private string name;
        private double price;
        public int Quantity { get; private set; }
        public Product(string name, double price, int quantity)
        {
            this.name = name;
            this.price = price;
            this.Quantity = quantity;
        }

        public Product(string productName)
        {
            this.name = productName;
            this.price = 0;
            this.Quantity = 0;
        }

        public override string ToString()
        {
            return string.Format("-> Name: {0} \t| Price: {1} \t| Quantity: {2}", name, price, Quantity);
        }

        internal double CalculatePrice()
        {
            return price * Quantity;
        }

        internal void AddQuantity()
        {
            Quantity++;
        }

        internal void DecreaseQuantity()
        {
            Quantity--;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   name == product.name;
        }
    }
}