using System.Text;

namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCart : IShoppingCart
    {
        private ShoppingCartId shoppingCartId;
        private ShoppingCartName shoppingCartName;
        private List<OrderItem> orderItems;
        
        public ShoppingCart(ShoppingCartId id)
        {
            this.shoppingCartId = id;
            this.shoppingCartName = ShoppingCartName.Create();
            this.orderItems = new List<OrderItem>();
        }

        public ShoppingCart(ShoppingCartId id, ShoppingCartName shoppingCartName, List<OrderItem> orderItems) : this(id)
        {
            this.shoppingCartName = shoppingCartName;
            this.orderItems = orderItems;
        }

        public ShoppingCartData ToPrimitives()
        {
            return new ShoppingCartData
            {
                ShoppingCartId = this.shoppingCartId.Value(),
                ShoppingCartName = this.shoppingCartName.Value(),
                OrderItems = this.orderItems.Select(x => x.ToPrimitives()).ToList()
        };
        }

        public static ShoppingCart FromPrimitives(ShoppingCartData data)
        {
            return new ShoppingCart(new ShoppingCartId(data.ShoppingCartId),
                                new ShoppingCartName(data.ShoppingCartName),
                                new List<OrderItem>(data.OrderItems.Select(x => OrderItem.FromPrimitives(x))));
        }

        public void AddProduct(Product product)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId() == product.GetProductId());
            if (findedOrderItem != null)
            {
                findedOrderItem.AddQuantity();
                return;
            }

            orderItems.Add(new OrderItem(product));
        }

        public string PrintProducts()
        {
            if (!orderItems.Any())
                return "No products";
            
            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            foreach (var item in orderItems.Select(x => x.ToPrimitives()))
                productList.AppendLine(string.Format("-> Name: {0} \t| Price: {1} \t| Quantity: {2}",
                                                     item.Product.ProductName,
                                                     item.Product.ProductPrice,
                                                     item.Quantity));

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

        public void DeleteProduct(Product product)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId() == product.GetProductId());
            
            if(findedOrderItem == null) 
                throw new Exception(string.Format("Error: The product with id: {0} does not exists in shoppingCart with id: {1}",
                                                  product.GetProductId().Value(),
                                                  shoppingCartId.Value()));
            
            if (findedOrderItem.IsQuantityGreaterThanOne())
            {
                findedOrderItem.DecreaseQuantity();
                return;
            }

            orderItems.Remove(findedOrderItem);
        }

        public override bool Equals(object? obj)
        {
            return obj is ShoppingCart cart &&
                   shoppingCartName == cart.shoppingCartName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(shoppingCartName);
        
        }
        private int GetTotalOfProducts()
        {
            return orderItems.Sum(x => x.GetQuantity());
        }

        public double ApplyDiscount(Discount discount)
        {
            double totalPrice = GetTotalPrice();
            if(totalPrice == 0)
                throw new Exception("Error: Can not apply discount to an empty ShoppingCart");

            totalPrice -= totalPrice * discount.GetCalculatedDiscount();

            return Math.Round(totalPrice, 2);
        }
    }

    public class ShoppingCartData
    {
        public string ShoppingCartId { get; set; }
        public string ShoppingCartName { get; set; }
        public List<OrderItemData> OrderItems { get; set; }
    }
}