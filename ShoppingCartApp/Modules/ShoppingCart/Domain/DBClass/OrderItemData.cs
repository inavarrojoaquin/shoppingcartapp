namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass
{
    public class OrderItemData
    {
        public string OrderItemId { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

        public string ProductId { get; set; }
        public ProductData Product { get; set;} = null!;

        public string ShoppingCartId { get; set; }
        public ShoppingCartData ShoppingCart { get; set; } = null!;
    }
}