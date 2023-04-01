namespace ShoppingCartApp.App.Domain
{
    public class Product
    {
        private string name;
        private double price;

        public Product(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public Product(string productName)
        {
            this.name = productName;
            this.price = 0;            
        }

        public override string ToString()
        {
            return string.Format("-> Name: {0} \t| Price: {1}", name, price);
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   name == product.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }

        internal double CalculatePrice(int quantity)
        {
            return price * quantity; 
        }
    }
}