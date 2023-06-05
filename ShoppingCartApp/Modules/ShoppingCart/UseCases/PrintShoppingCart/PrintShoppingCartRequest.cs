using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
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