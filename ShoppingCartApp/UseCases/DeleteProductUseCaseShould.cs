using NSubstitute;
using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    internal class DeleteProductUseCaseShould
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;
        private IBaseUseCase<DeleteProductRequest> deleteProductUseCase;
        
        [SetUp]
        public void SetUp() 
        {
            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministrator>();
            shoppingCartAdministrator.DeleteProductFromShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());

            deleteProductUseCase = new DeleteProductUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void DeleteProductToShoppingCartSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductName = "Test",
                ShoppingCartName = "Test",
            };
            DeleteProductRequest productRequest = new DeleteProductRequest(productDTO);
            
            Assert.DoesNotThrow(() => deleteProductUseCase.Execute(productRequest));

            shoppingCartAdministrator.Received(1).DeleteProductFromShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());
        }

        [Test]
        public void RaiseExWhenDeleteProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => deleteProductUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest))));
        }
    }
}
