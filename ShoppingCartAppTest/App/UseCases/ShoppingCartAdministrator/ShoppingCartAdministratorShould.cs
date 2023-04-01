using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartAppTest.App.UseCases.ShoppingCartAdministrator
{
    public class ShoppingCartAdministratorShould
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;
        private List<ShoppingCart> shoppingCartList;
        private List<Discount> discountList;

        [SetUp]
        public void Setup()
        {
            shoppingCartList = new List<ShoppingCart>();
            ShoppingCarts shoppingCarts = new ShoppingCarts(shoppingCartList);
            discountList = new List<Discount>();
            Discounts discounts = new Discounts(discountList);
            shoppingCartAdministrator = new ShoppingCartAdministratorService(shoppingCarts, discounts);
        }

        [Test]
        public void AddProductsToShoppingCart()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            ShoppingCart shoppingCart = new ShoppingCart("First", orderItems);
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new OrderItem(new Product("Corn", 1.50)));

            Assert.That(shoppingCartList.Count, Is.EqualTo(1));
            Assert.That(orderItems.Count, Is.EqualTo(5));
            Assert.That(shoppingCart.GetTotalPrice(), Is.EqualTo(7.11));
        }

        [Test]
        public void AddProductsToDifferentShoppingCart()
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            List<OrderItem> orderItemsSecond = new List<OrderItem>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", orderItemsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Iceberg", 2.50)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Tomatoe", 1.00)));

            Assert.That(shoppingCartList.Count, Is.EqualTo(2));
            Assert.That(orderItemsFirst.Count, Is.EqualTo(5));
            Assert.That(orderItemsSecond.Count, Is.EqualTo(2));
            Assert.That(firstShoppingCart.GetTotalPrice(), Is.EqualTo(7.11));
            Assert.That(secondShoppingCart.GetTotalPrice(), Is.EqualTo(3.5));
        }

        [Test]
        public void AddSameProductsToShoppingCart()
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            Assert.That(shoppingCartList.Count, Is.EqualTo(1));
            Assert.That(orderItemsFirst.Count, Is.EqualTo(5));
            Assert.That(orderItemsFirst.First(x => x.Equals(new OrderItem(new Product("Iceberg")))).GetQuantity(), Is.EqualTo(3));
            Assert.That(orderItemsFirst.First(x => x.Equals(new OrderItem(new Product("Bread")))).GetQuantity(), Is.EqualTo(2));
            Assert.That(firstShoppingCart.GetTotalPrice(), Is.EqualTo(12.33));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToTheShoppingCart(double discountQuantity, string discountName, double totalPrice)
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discountQuantity));

            Assert.That(discountList.Count, Is.EqualTo(1));
            Assert.That(discountList.First().ToString(), Is.EqualTo(string.Format("Promotion: {0}% off with code {1}", discountQuantity, discountName)));
            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(firstShoppingCart), Is.EqualTo(totalPrice));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToDifferentShoppingCart(double discountQuantity, string discountName, double totalPrice)
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            List<OrderItem> orderItemsSecond = new List<OrderItem>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", orderItemsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discountQuantity));

            Assert.That(discountList.Count, Is.EqualTo(1));
            Assert.That(discountList.First().ToString(), Is.EqualTo(string.Format("Promotion: {0}% off with code {1}", discountQuantity, discountName)));
            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(firstShoppingCart), Is.EqualTo(totalPrice));
            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(secondShoppingCart), Is.EqualTo(totalPrice));
        }

        [Test]
        public void ApplyMultiDiscountsAtTheSameTimeToTheShoppingCart()
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);

            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            const string PROMO_5 = "PROMO_5";
            const string PROMO_10 = "PROMO_10";
            shoppingCartAdministrator.ApplyDiscount(new Discount(PROMO_5, 5));
            shoppingCartAdministrator.ApplyDiscount(new Discount(PROMO_10, 10));

            Assert.That(discountList.Count, Is.EqualTo(2));
            Assert.That(discountList.First(x => x.Equals(new Discount(PROMO_5))).ToString(), Is.EqualTo(string.Format("Promotion: {0}% off with code {1}", 5, PROMO_5)));
            Assert.That(discountList.First(x => x.Equals(new Discount(PROMO_10))).ToString(), Is.EqualTo(string.Format("Promotion: {0}% off with code {1}", 10, PROMO_10)));

            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(firstShoppingCart), Is.EqualTo(10.48));
        }

        [Test]
        public void RaiseExWhenDiscountIsAlreadyApplied()
        {
            shoppingCartAdministrator.ApplyDiscount(new Discount("PROMO_5", 5));
            var ex = Assert.Throws<Exception>(() => shoppingCartAdministrator.ApplyDiscount(new Discount("PROMO_5", 5)));

            Assert.That(ex.Message, Does.Contain("The discount is already applied"));
        }

        [TestCase("Iceberg")]
        [TestCase("Chicken")]
        public void DeleteProductFromShoppingCart(string productName)
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));

            shoppingCartAdministrator.DeleteProductFromShoppingCart(firstShoppingCart, new OrderItem(new Product(productName)));

            if (productName == "Iceberg")
            {
                Assert.That(orderItemsFirst.Find(x => x.Equals(new OrderItem(new Product(productName)))).GetQuantity(), Is.EqualTo(2));
                Assert.That(orderItemsFirst.Count, Is.EqualTo(3));
                return;
            }

            if (productName == "Chicken")
            {
                Assert.That(orderItemsFirst.Find(x => x.Equals(new OrderItem(new Product("Iceberg")))).GetQuantity(), Is.EqualTo(3));
                Assert.That(orderItemsFirst.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void RaiseExWhenDeletingProductButShoppingCartDoesNotExists()
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("not_exists", orderItemsFirst);
            var ex = Assert.Throws<Exception>(() => shoppingCartAdministrator.DeleteProductFromShoppingCart(firstShoppingCart, null));

            Assert.That(ex.Message, Does.Contain("Error ShoppingCart does not exists when deleting product"));
        }

        [Test]
        public void PrintEmptyShoppingCart()
        {
            ShoppingCart shoppingCart = new ShoppingCart("First", new List<OrderItem>());
            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(shoppingCart);

            Assert.That(shoppingCartResult, Does.Contain("No products"));
            Assert.That(shoppingCartResult, Does.Contain("Total of products: 0"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain("Total price: 0"));
        }

        [Test]
        public void PrintShoppingCartWithProducts()
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);

            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(firstShoppingCart);

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));
        }

        [Test]
        public void PrintMultipleShoppingCartWithProducts()
        {
            List<OrderItem> orderItemFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            List<OrderItem> orderItemsSecond = new List<OrderItem>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", orderItemsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Iceberg", 2.50)));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new OrderItem(new Product("Tomatoe", 1.00)));

            string firstShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(firstShoppingCart);

            Assert.That(firstShoppingCartResult, Does.Contain("Products: "));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(firstShoppingCartResult, Does.Contain("No promotion"));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));

            string secondShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(secondShoppingCart);

            Assert.That(secondShoppingCartResult, Does.Contain("Products: "));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 2)));
            Assert.That(secondShoppingCartResult, Does.Contain("No promotion"));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total price: {0}", 3.5)));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void PrintShoppingCartWithProductsAndDiscount(int discountQuantity, string discountName, double totalPrice)
        {
            List<OrderItem> orderItemsFirst = new List<OrderItem>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", orderItemsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Iceberg", 2.17)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Tomatoe", 0.73)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Chicken", 1.83)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Bread", 0.88)));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new OrderItem(new Product("Corn", 1.50)));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discountQuantity));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(firstShoppingCart);

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discountQuantity, discountName)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }
    }
}