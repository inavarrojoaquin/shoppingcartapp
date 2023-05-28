using ShoppingCartApp.App.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events;

public class CloseShoppingCartEvent : IDomainEvent
{
    public readonly ShoppingCartData shoppingCartData;

    public CloseShoppingCartEvent(ShoppingCartData shoppingCartData)
    {
        this.shoppingCartData = shoppingCartData;
    }
}