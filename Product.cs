namespace ShoppingCartApp
{
    public class Product : ICloneable<Product>
    {
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; internal set; }
        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return string.Format("-> Name: {0} \t| Price: {1} \t| Quantity: {2}", Name, Price, Quantity);
        }

        internal double CalculatePrice()
        {
            return Price * Quantity;
        }

        internal void AddQuantity()
        {
            Quantity++;
        }

        public Product Clone()
        {
            return new Product(Name, Price, Quantity);
        }
    }
}