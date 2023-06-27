using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events
{
    public class StockUpdated : IDomainEvent
    {
        public ProductData ProductData { get; set; }
    }
}