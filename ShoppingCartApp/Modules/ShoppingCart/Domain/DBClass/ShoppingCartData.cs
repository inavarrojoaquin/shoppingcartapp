namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass
{
    public class ShoppingCartData
    {
        public string ShoppingCartId { get; set; }
        public string ShoppingCartName { get; set; }
        public bool IsClosed { get; set; }

        public ICollection<OrderItemData> OrderItems { get; set; } = new List<OrderItemData>();
    }
}