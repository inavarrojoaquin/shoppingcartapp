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
            List<Product> products= new List<Product>
            {
                new Product{ Name = "Iceberg", Price = 2.17, Quantity = 1 },
                new Product{ Name = "Tomatoe", Price = 0.73, Quantity = 1 },
                new Product{ Name = "Chicken", Price = 1.83, Quantity = 1 },
                new Product{ Name = "Bread", Price = 0.88, Quantity = 1 },
                new Product{ Name = "Corn", Price = 1.50, Quantity = 1 },
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
                new Product{ Name = "Iceberg", Price = 2.17, Quantity = 1 },
                new Product{ Name = "Iceberg", Price = 2.17, Quantity = 1 },
                new Product{ Name = "Iceberg", Price = 2.17, Quantity = 1 },
                new Product{ Name = "Tomatoe", Price = 0.73, Quantity = 1 },
                new Product{ Name = "Chicken", Price = 1.83, Quantity = 1 },
                new Product{ Name = "Bread", Price = 0.88, Quantity = 1 },
                new Product{ Name = "Bread", Price = 0.88, Quantity = 1 },
                new Product{ Name = "Corn", Price = 1.50, Quantity = 1 },
            };

            shoppingCart.AddProducts(products);

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            int totalOfProducts = products.Sum(x => x.Quantity);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", totalOfProducts)));
            double totalPrice = Math.Round(products.Sum(x => x.Price * x.Quantity), 2);
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }
    }
}