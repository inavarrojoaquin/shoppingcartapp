﻿using Microsoft.Extensions.Logging;
using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.AddProduct
{
    public class AddProductUseCase : IBaseUseCase<AddProductRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public AddProductUseCase(IProductRepository productRepository,
                                 IShoppingCartRepository shoppingCartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task ExecuteAsync(AddProductRequest productRequest)
        {
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(AddProductRequest)));

            Product product = productRepository.GetProductById(productRequest.ProductId);

            if (product == null) throw new Exception("Error: Product is null");

            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(productRequest.ShoppingCartId);

            if (shoppingCart == null)
                shoppingCart = new ShoppingCart(productRequest.ShoppingCartId);

            shoppingCart.AddProduct(product);

            await shoppingCartRepository.SaveAsync(shoppingCart);
        }
    }
}