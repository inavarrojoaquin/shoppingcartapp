namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Domain
{
    public class ShoppingCartId
    {
        private Guid guid;

        public ShoppingCartId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ShoppingCartId"));

            guid = Guid.Parse(id);
        }

        public static ShoppingCartId Create()
        {
            return new ShoppingCartId(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return guid.ToString();
        }
    }
}