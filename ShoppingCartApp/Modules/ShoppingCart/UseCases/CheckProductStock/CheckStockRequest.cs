using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CheckProductStock
{
    public class CheckStockRequest : IBaseRequest
    {
        public ShoppingCartData ShoppingCartData { get; }
        public CheckStockRequest(ShoppingCartData shoppingCartData)
        {
            ShoppingCartData = shoppingCartData;
        }
    }
}