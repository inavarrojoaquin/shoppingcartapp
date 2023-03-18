using System.Linq;

namespace ShoppingCartApp
{
    public class ShoppingCartShould
    {
        private IShoppingCart shoppingCart;


        [SetUp]
        public void Setup()
        {
            shoppingCart = new ShoppingCart();
        }

        [Test]
        public void PrintEmptyShoppingCart()
        {
            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("No products"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain("Total of products: 0"));
            Assert.That(shoppingCartResult, Does.Contain("Total price: 0"));
        }

        [Test]
        public void AddProductsToShoppingCart()
        {
            List<Product> products = new List<Product>
            {
                new Product("Iceberg", 2.17, 1 ),
                new Product("Tomatoe", 0.73, 1),
                new Product("Chicken", 1.83, 1),
                new Product("Bread", 0.88, 1),
                new Product("Corn", 1.50, 1),
            };

            shoppingCart.AddProducts(products);

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            int totalOfProducts = products.Sum(x => x.Quantity);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", totalOfProducts)));
            double totalPrice = Math.Round(products.Sum(x => x.Price * x.Quantity), 2);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }

        [Test]
        public void AddSameProductsToShoppingCart()
        {
            List<Product> products = new List<Product>
            {
                new Product("Iceberg", 2.17, 1),
                new Product("Iceberg", 2.17, 1),
                new Product("Iceberg", 2.17, 1),
                new Product("Tomatoe", 0.73, 1),
                new Product("Chicken", 1.83, 1),
                new Product("Bread", 0.88, 1),
                new Product("Bread", 0.88, 1),
                new Product("Corn", 1.50, 1),
            };

            shoppingCart.AddProducts(products);

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            int totalOfProducts = products.Sum(x => x.Quantity);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", totalOfProducts)));
            double totalPrice = Math.Round(products.Sum(x => x.CalculatePrice()), 2);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToTheShoppingCart(int discount, string promotion, double totalPrice)
        {
            List<Product> products = new List<Product>
            {
                new Product("Iceberg", 2.17, 1),
                new Product("Iceberg", 2.17, 1),
                new Product("Iceberg", 2.17, 1),
                new Product("Tomatoe", 0.73, 1),
                new Product("Chicken", 1.83, 1),
                new Product("Bread", 0.88, 1),
                new Product("Bread", 0.88, 1),
                new Product("Corn", 1.50, 1),
            };

            shoppingCart.AddProducts(products);
            shoppingCart.ApplyDiscount(promotion);

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discount, promotion)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }
    }
}