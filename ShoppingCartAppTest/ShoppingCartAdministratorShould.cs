using NSubstitute;

namespace ShoppingCartApp
{
    public class ShoppingCartAdministratorShould
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;
        private List<ShoppingCart> shoppingCartList;
        private List<Discount> discountList;

        [SetUp]
        public void Setup()
        {
            shoppingCartList = new List<ShoppingCart>();
            ShoppingCarts shoppingCarts = new ShoppingCarts(shoppingCartList);
            discountList = new List<Discount>();
            Discounts discounts = new Discounts(discountList);  
            shoppingCartAdministrator = new ShoppingCartAdministrator(shoppingCarts, discounts);
        }

        [Test]
        public void AddProductsToShoppingCart()
        {
            List<Product> products = new List<Product>();
            ShoppingCart shoppingCart = new ShoppingCart("First", products);
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, new Product("Corn", 1.50, 1));

            Assert.That(shoppingCartList.Count, Is.EqualTo(1));
            Assert.That(products.Count, Is.EqualTo(5));
            Assert.That(shoppingCart.GetTotalPrice(), Is.EqualTo(7.11));
        }

        [Test]
        public void AddProductsToDifferentShoppingCart()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

            List<Product> productsSecond = new List<Product>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", productsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Iceberg", 2.50, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Tomatoe", 1.00, 1));

            Assert.That(shoppingCartList.Count, Is.EqualTo(2));
            Assert.That(productsFirst.Count, Is.EqualTo(5));
            Assert.That(productsSecond.Count, Is.EqualTo(2));
            Assert.That(firstShoppingCart.GetTotalPrice(), Is.EqualTo(7.11));
            Assert.That(secondShoppingCart.GetTotalPrice(), Is.EqualTo(3.5));
        }

        [Test]
        public void AddSameProductsToShoppingCart()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

            Assert.That(shoppingCartList.Count, Is.EqualTo(1));
            Assert.That(productsFirst.Count, Is.EqualTo(5));
            Assert.That(productsFirst.First(x => x.Equals(new Product("Iceberg"))).Quantity, Is.EqualTo(3));
            Assert.That(productsFirst.First(x => x.Equals(new Product("Bread"))).Quantity, Is.EqualTo(2));
            Assert.That(firstShoppingCart.GetTotalPrice(), Is.EqualTo(12.33));
        }

        [TestCase(5, "PROMO_5", 11.71)]
        [TestCase(10, "PROMO_10", 11.1)]
        [TestCase(15, "PROMO_15", 10.48)]
        public void ApplyDiscountToTheShoppingCart(double discountQuantity, string discountName, double totalPrice)
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

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
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

            List<Product> productsSecond = new List<Product>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", productsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Corn", 1.50, 1));

            shoppingCartAdministrator.ApplyDiscount(new Discount(discountName, discountQuantity));

            Assert.That(discountList.Count, Is.EqualTo(1));
            Assert.That(discountList.First().ToString(), Is.EqualTo(string.Format("Promotion: {0}% off with code {1}", discountQuantity, discountName)));
            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(firstShoppingCart), Is.EqualTo(totalPrice));
            Assert.That(shoppingCartAdministrator.GetTotalPriceWithDiscounts(secondShoppingCart), Is.EqualTo(totalPrice));
        }

        [Test]
        public void ApplyMultiDiscountsAtTheSameTimeToTheShoppingCart()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);

            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

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
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));

            shoppingCartAdministrator.DeleteProductFromShoppingCart(firstShoppingCart, new Product(productName));

            if (productName == "Iceberg")
            {
                Assert.That(productsFirst.Find(x => x.Equals(new Product(productName))).Quantity, Is.EqualTo(2));
                Assert.That(productsFirst.Count, Is.EqualTo(3));
                return;
            }

            if (productName == "Chicken")
            {
                Assert.That(productsFirst.Find(x => x.Equals(new Product("Iceberg"))).Quantity, Is.EqualTo(3));
                Assert.That(productsFirst.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void RaiseExWhenDeletingProductButShoppingCartDoesNotExists()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("not_exists", productsFirst);
            var ex = Assert.Throws<Exception>(() => shoppingCartAdministrator.DeleteProductFromShoppingCart(firstShoppingCart, null));

            Assert.That(ex.Message, Does.Contain("Error ShoppingCart does not exists when deleting product"));
        }

        [Test]
        public void PrintEmptyShoppingCart()
        {
            ShoppingCart shoppingCart = new ShoppingCart("First", new List<Product>());
            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(shoppingCart);

            Assert.That(shoppingCartResult, Does.Contain("No products"));
            Assert.That(shoppingCartResult, Does.Contain("Total of products: 0"));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain("Total price: 0"));
        }

        [Test]
        public void PrintShoppingCartWithProducts()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);

            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

            string shoppingCartResult = shoppingCartAdministrator.PrintShoppingCart(firstShoppingCart);

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", 5)));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", 7.11)));
        }
        
        [Test]
        public void PrintMultipleShoppingCartWithProducts()
        {
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

            List<Product> productsSecond = new List<Product>();
            ShoppingCart secondShoppingCart = new ShoppingCart("Second", productsSecond);
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Iceberg", 2.50, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(secondShoppingCart, new Product("Tomatoe", 1.00, 1));

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
            List<Product> productsFirst = new List<Product>();
            ShoppingCart firstShoppingCart = new ShoppingCart("First", productsFirst);
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Iceberg", 2.17, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Tomatoe", 0.73, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Chicken", 1.83, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Bread", 0.88, 1));
            shoppingCartAdministrator.AddProductToShoppingCart(firstShoppingCart, new Product("Corn", 1.50, 1));

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