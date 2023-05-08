using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.AddProduct;

public class AddProductCommand : ICommand
{
    public ProductDTO productDTO;
}