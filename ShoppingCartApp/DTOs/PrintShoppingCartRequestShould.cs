namespace ShoppingCartApp.DTOs
{
    internal class PrintShoppingCartRequestShould
    {
        [Test]
        public void CreateRequestSuccessfully()
        {
            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO
            {
                ShoppingCartName = "Test"
            };

            PrintShoppingCartRequest request = new PrintShoppingCartRequest(shoppingCartDTO);
            
            Assert.That(string.IsNullOrEmpty(request.ShoppingCartName), Is.False);
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenNameIsNullOrEmpty(string name)
        {
            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO
            {
                ShoppingCartName = name
            };

            var ex = Assert.Throws<Exception>(() => new PrintShoppingCartRequest(shoppingCartDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "ShoppingCartName")));
        }
    }
}
