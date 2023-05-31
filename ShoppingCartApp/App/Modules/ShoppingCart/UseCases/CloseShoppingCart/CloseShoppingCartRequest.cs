using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
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