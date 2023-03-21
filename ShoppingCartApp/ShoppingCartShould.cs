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
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Tomatoe", 0.73, 1));
            shoppingCart.AddProduct(new Product("Chicken", 1.83, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Corn", 1.50, 1));

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));
        }

        [Test]
        public void AddSameProductsToShoppingCart()
        {
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Tomatoe", 0.73, 1));
            shoppingCart.AddProduct(new Product("Chicken", 1.83, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Corn", 1.50, 1));

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 12.33)));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToTheShoppingCart(int discount, string discountName, double totalPrice)
        {
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Tomatoe", 0.73, 1));
            shoppingCart.AddProduct(new Product("Chicken", 1.83, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Corn", 1.50, 1));

            shoppingCart.ApplyDiscount(new Discount(discountName, discount));

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discount, discountName)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }
        
        [Test]
        public void ApplyMultiDiscountsAtTheSameTimeToTheShoppingCart()
        {
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Tomatoe", 0.73, 1));
            shoppingCart.AddProduct(new Product("Chicken", 1.83, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Bread", 0.88, 1));
            shoppingCart.AddProduct(new Product("Corn", 1.50, 1));

            shoppingCart.ApplyDiscount(new Discount("PROMO_5", 5));
            shoppingCart.ApplyDiscount(new Discount("PROMO_10", 10));

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", 5, "PROMO_5")));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", 10, "PROMO_10")));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 10.48)));
        }

        [Test]
        public void RaiseExWhenDiscountIsAlreadyApplied()
        {
            shoppingCart.ApplyDiscount(new Discount("PROMO_5", 5));
            var ex = Assert.Throws<Exception>(() => shoppingCart.ApplyDiscount(new Discount("PROMO_5", 5)));

            Assert.That(ex.Message, Does.Contain("The discount is already applied"));
        }

        [TestCase("Iceberg")]
        [TestCase("Chicken")]
        public void DeleteProductFromShoppingCart(string productName)
        {
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Iceberg", 2.17, 1));
            shoppingCart.AddProduct(new Product("Chicken", 1.83, 1));
            shoppingCart.AddProduct(new Product("Tomatoe", 0.73, 1));

            shoppingCart.DeleteProduct(new Product(productName));

            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            if(productName == "Iceberg")
                Assert.That(shoppingCartResult, Does.Contain("Quantity: 2"));

            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 4)));
        }
    }
}