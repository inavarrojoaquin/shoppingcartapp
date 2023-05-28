using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.Close;

public class CloseShoppingCartRequest : IBaseRequest
{
    public string ShoppingCartId { get; set; }  
}