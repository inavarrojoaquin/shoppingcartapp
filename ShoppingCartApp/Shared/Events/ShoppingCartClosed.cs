using ShoppingCartApp.App.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events
{
    public class ShoppingCartClosed : IDomainEvent
    {
        private ShoppingCartData shoppingCartData;

        public ShoppingCartClosed(ShoppingCartData shoppingCartData)
        {
            this.shoppingCartData = shoppingCartData;
        }
    }
}