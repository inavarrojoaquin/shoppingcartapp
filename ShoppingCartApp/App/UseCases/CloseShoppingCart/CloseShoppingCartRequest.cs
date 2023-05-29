using ShoppingCartApp.App.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.CloseShoppingCart
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