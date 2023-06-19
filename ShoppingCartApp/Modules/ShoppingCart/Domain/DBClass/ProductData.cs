namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass
{
    public class ProductData
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductStock { get; set; }

        public ICollection<OrderItemData> OrderItems { get; } = new List<OrderItemData>();
    }
}