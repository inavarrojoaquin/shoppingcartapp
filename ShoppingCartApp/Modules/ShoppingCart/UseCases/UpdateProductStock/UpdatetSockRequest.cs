using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.UpdateProductStock
{
    public class UpdatetSockRequest : IBaseRequest
    {
        public ProductData ProductData { get; set; }
    }
}