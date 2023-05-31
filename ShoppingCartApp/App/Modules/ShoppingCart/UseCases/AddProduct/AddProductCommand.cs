using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.AddProduct
{
    public class AddProductCommand : ICommand
    {
        public ProductDTO ProductDTO { get; set; }
    }
}
