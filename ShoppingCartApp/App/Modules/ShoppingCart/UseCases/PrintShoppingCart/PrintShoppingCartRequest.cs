using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartRequest : IBaseRequest
    {
        public ShoppingCartId ShoppingCartId { get; }
        public PrintShoppingCartRequest(ShoppingCartDTO shoppingCartDTO)
        {
            ShoppingCartId = new ShoppingCartId(shoppingCartDTO.ShoppingCartId);
        }
    }
}