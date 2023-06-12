using ShoppingCartApp.Modules.ProductModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events
{
    public class ProductStockUpdated : IDomainEvent
    {
        public ProductData ProductData { get; set; }
    }
}