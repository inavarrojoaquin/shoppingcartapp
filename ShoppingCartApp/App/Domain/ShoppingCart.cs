using System.Text;

namespace ShoppingCartApp.App.Domain
{
    public class ShoppingCart
    {
        public ShoppingCartId ShoppingCartId { get; }
        public ShoppingCartName ShoppingCartName { get; }
        public List<OrderItem> OrderItems { get; }
        public Discount Discount { get; private set; }
        
        public ShoppingCart(ShoppingCartId id)
        {
            this.ShoppingCartId = id;
            this.ShoppingCartName = ShoppingCartName.Create();
            this.OrderItems = new List<OrderItem>();
        }

        public ShoppingCart(ShoppingCartId id, ShoppingCartName shoppingCartName, List<OrderItem> orderItems) : this(id)
        {
            this.ShoppingCartName = shoppingCartName;
            this.OrderItems = orderItems;
        }

        public ShoppingCartData ToPrimitives()
        {
            return new ShoppingCartData
            {
                ShoppingCartId = this.ShoppingCartId.Value(),
                ShoppingCartName = this.ShoppingCartName.Value(),
                OrderItems = this.OrderItems.Select(x => x.ToPrimitives()).ToList()
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
            OrderItem? findedOrderItem = OrderItems.FirstOrDefault(x => x.GetProductId() == product.GetProductId());
            if (findedOrderItem != null)
            {
                findedOrderItem.AddQuantity();
                return;
            }

            OrderItems.Add(new OrderItem(product));
        }
        
        public void DeleteProduct(Product product)
        {
            OrderItem? findedOrderItem = OrderItems.FirstOrDefault(x => x.GetProductId() == product.GetProductId());

            if (findedOrderItem == null)
                throw new Exception(string.Format("Error: The product with id: {0} does not exists in shoppingCart with id: {1}",
                                                  product.GetProductId().Value(),
                                                  ShoppingCartId.Value()));

            if (findedOrderItem.IsQuantityGreaterThanOne())
            {
                findedOrderItem.DecreaseQuantity();
                return;
            }

            OrderItems.Remove(findedOrderItem);
        }

        public void ApplyDiscount(Discount discount)
        {
            double totalPrice = GetTotalPrice();
            if (totalPrice == 0)
                throw new Exception("Error: Can not apply discount to an empty ShoppingCart");

            this.Discount = discount;
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
            return OrderItems.Sum(x => x.CalculatePrice());
        }

        private double GetTotalPriceWithDiscount()
        {
            double totalPrice = GetTotalPrice();
            if (this.Discount != null)
                totalPrice -= this.Discount.CalculateDiscount(totalPrice);

            return Math.Round(totalPrice, 2);
        }

        private string PrintProducts()
        {
            if (!OrderItems.Any())
                return "No products";

            StringBuilder productList = new();
            productList.AppendLine("Products: ");
            foreach (var item in OrderItems.Select(x => x.ToPrimitives()))
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
            if (Discount == null)
                return "No promotion";

            DiscountData discountData = Discount.ToPrimitives();
            return string.Format("Promotion: {0}% off with code {1}",
                                 discountData.DiscountQuantity,
                                 discountData.DiscountName);
        }

        private string PrintTotalPrice()
        {
            return string.Format("Total price: {0}", GetTotalPriceWithDiscount());
        }

        private int GetTotalOfProducts()
        {
            return OrderItems.Sum(x => x.GetQuantity());
        }
    }

    public class ShoppingCartData
    {
        public string ShoppingCartId { get; set; }
        public string ShoppingCartName { get; set; }
        public List<OrderItemData> OrderItems { get; set; }
        //TODO Agregar Discount
    }
}