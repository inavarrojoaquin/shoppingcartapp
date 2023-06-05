using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartCommand : ICommand
    {
        public ShoppingCartDTO ShoppingCartDTO { get; set; }
    }
}
