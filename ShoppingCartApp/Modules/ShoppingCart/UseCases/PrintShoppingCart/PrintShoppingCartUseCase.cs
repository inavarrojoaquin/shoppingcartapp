﻿using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartUseCase : IBaseUseCase<PrintShoppingCartRequest, string>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public PrintShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<string> ExecuteAsync(PrintShoppingCartRequest request)
        {
            if (request == null)
                throw new Exception(string.Format("Error: {0} can't be null", nameof(PrintShoppingCartRequest)));

            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(request.ShoppingCartId);

            return shoppingCart.Print();
        }
    }
}