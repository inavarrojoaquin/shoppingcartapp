namespace ShoppingCartApp.App.Domain
{
    public class OrderItemId
    {
        private readonly Guid guid;

        public OrderItemId(string id)
        {
            this.guid = Guid.Parse(id);
        }

        public static OrderItemId Create()
        {
            return new OrderItemId(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return guid.ToString();
        }
    }
}