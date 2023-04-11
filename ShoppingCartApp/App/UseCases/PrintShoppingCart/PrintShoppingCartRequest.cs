using ShoppingCartApp.App.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart
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