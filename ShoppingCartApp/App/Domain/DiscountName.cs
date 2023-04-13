namespace ShoppingCartApp.App.Domain
{
    public class DiscountName
    {
        private readonly string name;

        public DiscountName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "DiscountName"));

            this.name = name;
        }
        
        public static DiscountName Create()
        {
            return new DiscountName(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return name;
        }
    }
}