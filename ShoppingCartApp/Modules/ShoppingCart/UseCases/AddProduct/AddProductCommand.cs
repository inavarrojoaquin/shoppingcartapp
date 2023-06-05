using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct
{
    public class AddProductCommand : ICommand
    {
        public ProductDTO ProductDTO { get; set; }
    }
}
