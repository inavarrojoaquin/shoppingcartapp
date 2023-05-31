using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartCommand : ICommand
    {
        public ShoppingCartDTO ShoppingCartDTO { get; set; }
    }
}
