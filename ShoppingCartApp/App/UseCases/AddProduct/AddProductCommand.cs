using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.App.UseCases.AddProduct
{
    public class AddProductCommand : ICommand
    {
        public ProductDTO ProductDTO { get; set; }
    }
}
