namespace ShoppingCartApp.Modules.ProductModule.Domain
{
    public class Name
    {
        private readonly string name;

        public Name(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "Name"));

            this.name = name;
        }

        public static Name Create()
        {
            return new Name(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return name;
        }
    }
}