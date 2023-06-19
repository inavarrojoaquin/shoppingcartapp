using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock
{
    public class CheckStockRequest : IBaseRequest
    {
        public ShoppingCartData ShoppingCartData { get; set; }
    }
}