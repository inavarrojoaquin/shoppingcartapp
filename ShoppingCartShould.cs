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
        public void GetShoppingCartEmptyList()
        {
            Assert.That(shoppingCart.GetProducts().Count, Is.EqualTo(0));
            Assert.That(shoppingCart.GetPromotion(), Is.Empty);
            Assert.That(shoppingCart.GetTotalOfProducts(), Is.EqualTo(0));
            Assert.That(shoppingCart.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void AddProductToShoppingCart()
        {

        }
    }
}