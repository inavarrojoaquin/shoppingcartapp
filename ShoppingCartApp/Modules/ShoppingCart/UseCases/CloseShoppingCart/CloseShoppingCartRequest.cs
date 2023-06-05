using ShoppingCartApp.DTOs;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartRequest : IBaseRequest
    {
        public ShoppingCartId ShoppingCartId { get; }

        public CloseShoppingCartRequest(ShoppingCartDTO shoppingCartDTO)
        {
            ShoppingCartId = new ShoppingCartId(shoppingCartDTO.ShoppingCartId);
        }
    }
}