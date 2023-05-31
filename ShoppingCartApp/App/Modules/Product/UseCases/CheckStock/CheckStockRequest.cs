using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockRequest : IBaseRequest
    {
        public ShoppingCartData ShoppingCartData { get; set; }
    }
}