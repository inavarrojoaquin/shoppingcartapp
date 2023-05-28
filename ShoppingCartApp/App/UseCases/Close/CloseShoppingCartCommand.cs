using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.Close;

public class CloseShoppingCartCommand : ICommand
{
    public string ShoppingCartId { get; set; }
}