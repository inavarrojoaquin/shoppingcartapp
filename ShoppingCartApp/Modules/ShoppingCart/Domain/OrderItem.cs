namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class OrderItem
    {
        private OrderItemId orderItemId;
        private ProductId productId;
        private ProductPrice productPrice;
        private Quantity quantity;
        private OrderItemData orderItemData;

        public OrderItem(OrderItemId orderItemId, ProductId productId, ProductPrice productPrice, Quantity quantity)
        {
            this.orderItemId = orderItemId;
            this.quantity = quantity;
            this.productId = productId;
            this.productPrice = productPrice;
            orderItemData = new OrderItemData();
        }

        public static OrderItem Create(Product product)
        {
            return new OrderItem(OrderItemId.Create(), product.GetProductId(), new ProductPrice(product.GetPrice()),
                Quantity.Create());
        }

        public OrderItemData ToPrimitives()
        {
            orderItemData.OrderItemId = orderItemId.Value();
            orderItemData.Quantity = quantity.Value();
            orderItemData.ProductId = productId.Value();
            orderItemData.ProductPrice = productPrice.Value();
            return orderItemData;
        }

        public static OrderItem FromPrimitives(OrderItemData orderItemData)
        {
            return new OrderItem(new OrderItemId(orderItemData.OrderItemId),
                                 new ProductId(orderItemData.ProductId),
                                 new ProductPrice(orderItemData.ProductPrice),
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
            return productPrice.Value() * quantity.Value();
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
            return productId;
        }
    }

    public class OrderItemData
    {
        public string OrderItemId { get; set; }
        public string ProductId { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }


        public string ShoppingCartId { get; set; }
        public ShoppingCartData ShoppingCartData { get; set; }
    }
}