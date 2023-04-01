using System.Text;

namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly string name;
        private List<OrderItem> orderItems;

        public ShoppingCart(string name, List<OrderItem> orderItems)
        {
            this.name = name;
            this.orderItems = orderItems;
        }

        public void AddProduct(OrderItem orderItem)
        {
            if (orderItems.Contains(orderItem))
            {
                OrderItem findedOrderItem = orderItems.First(x => x.Equals(orderItem));
                findedOrderItem.AddQuantity();
                return;
            }

            orderItems.Add(orderItem);
        }

        public string PrintProducts()
        {
            if (!orderItems.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            foreach (var item in orderItems)
                productList.AppendLine(item.ToString());

            return productList.ToString();
        }

        public string PrintTotalNumberOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
        }

        public double GetTotalPrice()
        {
            return orderItems.Sum(x => x.CalculatePrice());
        }

        public void DeleteProduct(OrderItem orderItem)
        {
            OrderItem findedOrderItem = orderItems.First(x => x.Equals(orderItem));
            
            if (findedOrderItem.IsQuantityGreaterThanOne())
            {
                findedOrderItem.DecreaseQuantity();
                return;
            }

            orderItems.Remove(orderItem);
        }

        public override bool Equals(object? obj)
        {
            return obj is ShoppingCart cart &&
                   name == cart.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        
        }
        private int GetTotalOfProducts()
        {
            return orderItems.Sum(x => x.GetQuantity());
        }
    }
}