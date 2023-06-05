namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class ProductPrice
    {
        private readonly double price;

        public ProductPrice(double price)
        {
            if (price < 0)
                throw new Exception(string.Format("Error: {0} can not be less than 0", "ProductPrice"));

            this.price = price;
        }

        public static ProductPrice Create()
        {
            return new ProductPrice(0);
        }
        public double Value()
        {
            return price;
        }
    }
}