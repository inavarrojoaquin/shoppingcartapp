namespace ShoppingCartApp.App.Domain
{
    public class OrderItem
    {
        private OrderItemId orderItemId;
        private Product product;
        private Quantity quantity;
        private OrderItemData orderItemData;
        
        public OrderItem(OrderItemId orderItemId, Product product, Quantity quantity)
        {
            this.orderItemId = orderItemId;
            this.product = product;
            this.quantity = quantity;
            this.orderItemData = new OrderItemData();
        }

        public OrderItemData ToPrimitives()
        {
            orderItemData.OrderItemId = this.orderItemId.Value();
            orderItemData.Product = this.product.ToPrimitives();
            orderItemData.Quantity = this.quantity.Value();

            return orderItemData;
        }

        public static OrderItem FromPrimitives(OrderItemData orderItemData)
        {
            return new OrderItem(new OrderItemId(orderItemData.OrderItemId),
                                 Product.FromPrimitives(orderItemData.Product),
                                 new Quantity(orderItemData.Quantity));
        }

        public void AddQuantity()
        {
            quantity.AddQuantity();
        }
        public void DecreaseQuantity()
        {
            quantity.DecreaseQuantity();
        }
        public double CalculatePrice()
        {
            return product.GetPrice() * quantity.Value();
        }

        public bool IsQuantityGreaterThanOne()
        {
            return quantity.Value() > 1;
        }

        public int GetQuantity()
        {
            return quantity.Value();
        }

        public ProductId GetProductId()
        {
            return product.GetProductId();
        }
        public OrderItemId GetOrderItemId()
        {
            return orderItemId;
        }
    }

    public class OrderItemData
    {
        public string OrderItemId { get; set; }
        public ProductData Product { get; set; }
        public int Quantity { get; set; }
    }
}