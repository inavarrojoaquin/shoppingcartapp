using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart;

public class PrintShoppingCartQuery : IQuery<string>
{
    public string  ShoppingCartId { get; set; } 
}