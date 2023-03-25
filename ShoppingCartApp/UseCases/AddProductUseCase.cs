﻿using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public class AddProductUseCase : IAddProductUseCase
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;

        public AddProductUseCase(IShoppingCartAdministrator shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(AddProductRequest productRequest)
        {
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(AddProductRequest)));

            Product product = new Product(productRequest.Name, productRequest.Price, productRequest.Quantity);
            ShoppingCart shoppingCart = new ShoppingCart(productRequest.ShoppingCartName);
            
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, product);
        }
    }
}