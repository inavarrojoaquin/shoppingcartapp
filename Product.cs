namespace ShoppingCartApp
{
    public class Product
    {
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; }
        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}