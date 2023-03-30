using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.DTOs;

namespace ShoppingCartAppTest.App.UseCases.DeleteProduct
{
    internal class DeleteProductRequestShould
    {
        [Test]
        public void CreateDeleteProductRequestSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ShoppingCartName = "Test"
            };

            DeleteProductRequest productRequest = new DeleteProductRequest(productDTO);

            Assert.That(string.IsNullOrEmpty(productRequest.Name), Is.False);
            Assert.That(string.IsNullOrEmpty(productRequest.ShoppingCartName), Is.False);
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenProductNameIsNullOrEmpty(string name)
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = name,
                ShoppingCartName = "Test",
            };

            var ex = Assert.Throws<Exception>(() => new DeleteProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "ProductName")));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenShoppingCartNameIsNullOrEmpty(string name)
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ShoppingCartName = name,
            };

            var ex = Assert.Throws<Exception>(() => new DeleteProductRequest(productDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "ShoppingCartName")));
        }
    }
}
