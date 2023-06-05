using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartQuery : IQuery<string>
    {
        public string ShoppingCartId { get; set; }
    }
}
