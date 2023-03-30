using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartRequest : IBaseRequest
    {
        public string ShoppingCartName { get; }
        public PrintShoppingCartRequest(ShoppingCartDTO shoppingCartDTO)
        {
            if (string.IsNullOrEmpty(shoppingCartDTO.ShoppingCartName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ShoppingCartName"));

            ShoppingCartName = shoppingCartDTO.ShoppingCartName;
        }
    }
}