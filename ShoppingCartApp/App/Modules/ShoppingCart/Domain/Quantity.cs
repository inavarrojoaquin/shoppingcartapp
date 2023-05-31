namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Domain
{
    public class Quantity
    {
        private int quantity;

        public Quantity(int quantity)
        {
            if (quantity < 0)
                throw new Exception(string.Format("Error: {0} can not be less than 0", "Quantity"));

            this.quantity = quantity;
        }

        public static Quantity Create()
        {
            return new Quantity(1);
        }
        public int Value()
        {
            return quantity;
        }

        public Quantity AddQuantity()
        {
            return new Quantity(quantity++);
        }

        public Quantity DecreaseQuantity()
        {
            return new Quantity(quantity--);
        }
    }
}