﻿namespace ShoppingCartApp.App.Domain
{
    public class OrderItems
    {
        private List<OrderItem> orderItems;
        public OrderItems(List<OrderItem> orderItems)
        {
            this.orderItems = orderItems;
        }

        public static OrderItems Create()
        {
            return new OrderItems(new List<OrderItem>());
        }

        public static OrderItems FromPrimitives(List<OrderItemData> orderItemsData)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            orderItemsData.ForEach(x => orderItems.Add(OrderItem.FromPrimitives(x)));
            
            return new OrderItems(orderItems);
        }

        public List<OrderItemData> ToPrimitives() 
        {
            return this.orderItems.Select(x => x.ToPrimitives()).ToList();
        }

        internal void AddProduct(ProductId productId)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId() == productId);
            if (findedOrderItem != null)
            {
                findedOrderItem.AddQuantity();
                return;
            }

            orderItems.Add(new OrderItem(new Product(productId, ProductName.Create(), ProductPrice.Create())));
        }
    }
}