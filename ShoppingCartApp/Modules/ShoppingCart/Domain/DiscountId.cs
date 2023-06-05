namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class DiscountId
    {
        private readonly Guid guid;

        public DiscountId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "DiscountId"));

            guid = Guid.Parse(id);
        }

        public static DiscountId Create()
        {
            return new DiscountId(Guid.NewGuid().ToString());
        }

        public string Value()
        {
            return guid.ToString();
        }
    }
}