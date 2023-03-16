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
                new Product("Iceberg", 2.17, 1),
                new Product("Tomatoe", 0.73, 1),
                new Product("Chicken", 1.83, 1),
                new Product("Bread", 0.88, 1),
                new Product("Corn", 1.50, 1),
            };

            products.ForEach(x => shoppingCart.AddProduct(x));
            
            string shoppingCartResult = shoppingCart.PrintShoppingCart();

            Assert.That(shoppingCartResult, Does.Contain("Products: "));
            Assert.That(shoppingCartResult, Does.Contain("No promotion"));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total of products: {0}", products.Count)));
            Assert.That(shoppingCartResult, Does.Contain(string.Format("Total price: {0}", products.Sum(x => x.Price))));
        }
    }
}