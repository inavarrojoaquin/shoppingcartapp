using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using System.Text;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Domain
{
    public class ShoppingCart : AggregateRoot
    {
        private ShoppingCartId shoppingCartId;
        private ShoppingCartName shoppingCartName;
        private List<OrderItem> orderItems;
        private Discount discount;
        private ShoppingCartData shoppingCartData;
        private bool isClosed;

        public ShoppingCartId GetShoppingCartId() => shoppingCartId;

        public ShoppingCart(ShoppingCartId id)
        {
            shoppingCartId = id;
            shoppingCartName = ShoppingCartName.Create();
            orderItems = new List<OrderItem>();
            isClosed = false;
            shoppingCartData = new();
        }

        public ShoppingCart(ShoppingCartId id, ShoppingCartName shoppingCartName, List<OrderItem> orderItems) : this(id)
        {
            this.shoppingCartName = shoppingCartName;
            this.orderItems = orderItems;
        }

        public ShoppingCartData ToPrimitives()
        {
            shoppingCartData.ShoppingCartId = shoppingCartId.Value();
            shoppingCartData.ShoppingCartName = shoppingCartName.Value();
            shoppingCartData.IsClosed = isClosed;
            shoppingCartData.OrderItems = orderItems.Select(x => x.ToPrimitives()).ToList();
            return shoppingCartData;
        }

        public static ShoppingCart FromPrimitives(ShoppingCartData data)
        {
            var shoppingCart = new ShoppingCart(new ShoppingCartId(data.ShoppingCartId),
                new ShoppingCartName(data.ShoppingCartName),
                new List<OrderItem>(data.OrderItems.Select(x => OrderItem.FromPrimitives(x))));
            shoppingCart.shoppingCartData = data;
            shoppingCart.isClosed = data.IsClosed;
            return shoppingCart;
        }

        public void AddProduct(Product product)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId().Value() == product.GetProductId().Value());
            if (findedOrderItem != null)
            {
                findedOrderItem.AddQuantity();
                return;
            }

            orderItems.Add(OrderItem.Create(product));
        }

        public void DeleteProduct(Product product)
        {
            OrderItem? findedOrderItem = orderItems.FirstOrDefault(x => x.GetProductId().Value() == product.GetProductId().Value());

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
            shoppingCartAdministratorBuilder.AppendLine(string.Format("Total price: {0}", GetTotalPriceWithDiscount()));

            return shoppingCartAdministratorBuilder.ToString();
        }

        public void Close()
        {
            isClosed = true;

            AddEvent(new ShoppingCartClosed { ShoppingCartData = shoppingCartData });
        }

        private double GetTotalPrice()
        {
            return orderItems.Sum(x => x.CalculatePrice());
        }

        private double GetTotalPriceWithDiscount()
        {
            double totalPrice = GetTotalPrice();

            if (discount != null)
                totalPrice -= totalPrice * discount.GetCalculatedDiscount();

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
                                                     "item.Product.ProductName",
                                                     item.ProductPrice,
                                                     item.Quantity));

            return productList.ToString();
        }

        private string PrintDiscount()
        {
            if (discount == null)
                return "No promotion";

            return discount.Print();
        }

        private string PrintTotalNumberOfProducts()
        {
            return string.Format("Total of products: {0}", GetTotalOfProducts());
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
        public bool IsClosed { get; set; }
        public List<OrderItemData> OrderItems { get; set; }
    }
}