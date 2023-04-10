namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCartName
    {
        private readonly string name;

        public ShoppingCartName(string name)
        {
            this.name = name;
        }

        public static ShoppingCartName Create()
        {
            return new ShoppingCartName(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return name;
        }
    }
}