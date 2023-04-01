namespace ShoppingCartApp.App.Domain
{
    public class OrderItem
    {
        private Product product;
        private int quantity;

        public OrderItem(Product product)
        {
            this.product = product;
            this.quantity = 1;
        }

        public OrderItem(Product product, int quantity) : this(product)
        {
            this.quantity = quantity;
        }

        public void AddQuantity()
        {
            quantity++;
        }
        public void DecreaseQuantity()
        {
            quantity--;
        }
        public override string ToString()
        {
            return product.ToString() + string.Format("\t| Quantity: {0}", quantity);
        }
        public double CalculatePrice()
        {
            return product.CalculatePrice(quantity);
        }

        public bool IsQuantityGreaterThanOne()
        {
            return quantity > 1;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public override bool Equals(object? obj)
        {
            return obj is OrderItem item &&
                   EqualityComparer<Product>.Default.Equals(product, item.product);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(product);
        }
    }
}