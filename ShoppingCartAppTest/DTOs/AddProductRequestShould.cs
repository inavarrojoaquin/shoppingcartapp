namespace ShoppingCartApp.DTOs
{
    internal class AddProductRequestShould
    {
        [Test]
        public void CreateProductRequestSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName= "Test",
                ProductPrice = 1,
                ProductQuantity = 1,
                ShoppingCartName = "Test",
            };

            AddProductRequest productRequest = new AddProductRequest(productDTO);
            
            Assert.That(string.IsNullOrEmpty(productRequest.Name), Is.False);
            Assert.That(productRequest.Price, Is.GreaterThanOrEqualTo(0));
            Assert.That(productRequest.Quantity, Is.GreaterThanOrEqualTo(0));
            Assert.That(string.IsNullOrEmpty(productRequest.ShoppingCartName), Is.False);
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenProductNameIsNullOrEmpty(string name)
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = name,
                ProductPrice = 1,
                ProductQuantity = 1,
                ShoppingCartName = "Test",
            };

            var ex = Assert.Throws<Exception>(() => new AddProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "ProductName")));
        }

        [Test]
        public void RaiseExWhenPriceIsLessThan0()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ProductPrice = -1,
                ProductQuantity = 1,
                ShoppingCartName = "Test",
            };

            var ex = Assert.Throws<Exception>(() => new AddProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be less than 0", "ProductPrice")));
        }

        [Test]
        public void RaiseExWhenWuantityIsLessThan0()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ProductPrice = 1,
                ProductQuantity = -1,
                ShoppingCartName = "Test",
            };

            var ex = Assert.Throws<Exception>(() => new AddProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be less than 0", "ProductQuantity")));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenShoppingCartNameIsNullOrEmpty(string name)
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ProductPrice = 1,
                ProductQuantity = 1,
                ShoppingCartName = name,
            };

            var ex = Assert.Throws<Exception>(() => new AddProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "ShoppingCartName")));
        }
    }
}
