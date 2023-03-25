﻿using NSubstitute;
using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    internal class AddProductUseCaseShould
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;
        private AddProductUseCase addProductUseCase;
        
        [SetUp]
        public void SetUp() 
        {
            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministrator>();
            shoppingCartAdministrator.AddProductToShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());

            addProductUseCase = new AddProductUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void AddProductToShoppingCartSuccessfully()
        {
            ProductDTO productDTO = new ProductDTO();
            ProductRequest productRequest = new ProductRequest(productDTO);
            
            Assert.DoesNotThrow(() => addProductUseCase.Execute(productRequest));

            shoppingCartAdministrator.Received(1).AddProductToShoppingCart(Arg.Any<ShoppingCart>(), Arg.Any<Product>());
        }

        [Test]
        public void RaiseExWhenProductRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => addProductUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(ProductRequest))));
        }
    }
}
