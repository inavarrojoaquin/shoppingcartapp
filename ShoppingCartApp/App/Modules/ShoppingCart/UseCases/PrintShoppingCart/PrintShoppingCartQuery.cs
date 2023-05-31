using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartQuery : IQuery<string>
    {
        public string ShoppingCartId { get; set; }
    }
}
