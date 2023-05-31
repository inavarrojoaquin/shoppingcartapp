using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.DeleteProduct
{
    public class DeleteProductCommand : ICommand
    {
        public ProductDTO ProductDTO { get; set; }
    }
}