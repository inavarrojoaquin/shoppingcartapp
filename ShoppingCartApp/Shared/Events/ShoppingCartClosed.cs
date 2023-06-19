using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events
{
    public class ShoppingCartClosed : IDomainEvent
    {
        public ShoppingCartData ShoppingCartData { get; set; }
    }
}