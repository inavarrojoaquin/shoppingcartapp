namespace ShoppingCartApp
{
    public class ShoppingCartAdministratorShould
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;


        [SetUp]
        public void Setup()
        {
            shoppingCartAdministrator = new ShoppingCartAdministrator();
        }

        [Test]
        public void PrintEmptyShoppingCart()
        {
            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            Assert.That(shoppingCartResult, Does.Contain("No products"));
            Assert.That(shoppingCartResult, Does.Contain("Total of products: 0"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain("Total price: 0"));
        }

        [Test]
        public void AddProductsToShoppingCart()
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));
        }

        [Test]
        public void AddProductsToDifferentShoppingCart()
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Iceberg", 2.50, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Tomatoe", 1.00, 1));

            string firstShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));
            string secondShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("Second"));

            Assert.That(firstShoppingCartResult, Does.Contain("Products: "));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));

            Assert.That(secondShoppingCartResult, Does.Contain("Products: "));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 2)));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total price: {0}", 3.5)));
        }

        [Test]
        public void AddSameProductsToShoppingCart()
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 12.33)));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToTheShoppingCart(int discount, string discountName, double totalPrice)
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discount));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("Quantity: 3"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discount, discountName)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToDifferentShoppingCart(int discount, string discountName, double totalPrice)
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("Second"), new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discount));

            string firstShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));
            string secondShoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("Second"));

            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discount, discountName)));
            Assert.That(firstShoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));

            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", discount, discountName)));
            Assert.That(secondShoppingCartResult, Does.Contain(string.Format("Total price: {0}", totalPrice)));
        }

        [Test]
        public void ApplyMultiDiscountsAtTheSameTimeToTheShoppingCart()
        {
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.ApplyDiscount(new Discount("PROMO_5", 5));
            shoppingCartAdministrator.ApplyDiscount(new Discount("PROMO_10", 10));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", 5, "PROMO_5")));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Promotion: {0}% off with code {1}", 10, "PROMO_10")));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 8)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 10.48)));
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
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(new ShoppingCart("First"), new Product("Tomatoe", 0.73, 1));

            shoppingCartAdministrator.DeleteProductFromShoppingCart(new ShoppingCart("First"), new Product(productName));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(new ShoppingCart("First"));

            if (productName == "Iceberg")
                Assert.That(shoppingCartResult, Does.Contain("Quantity: 2"));

            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 4)));
        }

        [Test]
        public void RaiseExWhenDeletingProductButShoppingCartDoesNotExists()
        {
            var ex = Assert.Throws<Exception>(() => shoppingCartAdministrator.DeleteProductFromShoppingCart(new ShoppingCart("not_exists"), null));

            Assert.That(ex.Message, Does.Contain("Error ShoppingCart does not exists when deleting product"));
        }
    }
}