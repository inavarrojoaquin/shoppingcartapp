using System.Text;

namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCart
    {
        private ShoppingCartId shoppingCartId;
        private ShoppingCartName shoppingCartName;
        private List<OrderItem> orderItems;
        private Discount discount;
        
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
        
        public void DeleteProduct(Product product)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId() == product.GetProductId());

            if (findedOrderItem == null)
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

        public void ApplyDiscount(Discount discount)
        {
            double totalPrice = GetTotalPrice();
            if (totalPrice == 0)
                throw new Exception("Error: Can not apply discount to an empty ShoppingCart");

            this.discount = discount;
        }

        public string Print()
        {
            StringBuilder shoppingCartAdministratorBuilder = new();
            shoppingCartAdministratorBuilder.AppendLine(PrintProducts());
            shoppingCartAdministratorBuilder.AppendLine(PrintTotalNumberOfProducts());
            shoppingCartAdministratorBuilder.AppendLine(PrintDiscount());
            shoppingCartAdministratorBuilder.AppendLine(PrintTotalPrice());

            return shoppingCartAdministratorBuilder.ToString();
        }

        private double GetTotalPrice()
        {
            return orderItems.Sum(x => x.CalculatePrice());
        }

        private double GetTotalPriceWithDiscount()
        {
            double totalPrice = GetTotalPrice();
            if (this.discount != null)
                totalPrice -= this.discount.CalculateDiscount(totalPrice);

            return Math.Round(totalPrice, 2);
        }

        private string PrintProducts()
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
        private string PrintTotalNumberOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
        }

        private string PrintDiscount()
        {
            if (discount == null)
                return "No promotion";

            DiscountData discountData = discount.ToPrimitives();
            return string.Format("Promotion: {0}% off with code {1}",
                                 discountData.Quantity.Value(),
                                 discountData.Name.Value());
        }

        private string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", GetTotalPriceWithDiscount());
        }

        private int GetTotalOfProducts()
        {
            return orderItems.Sum(x => x.GetQuantity());
        }
    }

    public class ShoppingCartData
    {
        public string ShoppingCartId { get; set; }
        public string ShoppingCartName { get; set; }
        public List<OrderItemData> OrderItems { get; set; }
    }
}