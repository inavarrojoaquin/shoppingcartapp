using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.App.UseCases.AddProduct
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IBaseUseCase<AddProductRequest> useCase;

        public AddProductCommandHandler(IBaseUseCase<AddProductRequest> useCase)
        {
            this.useCase = useCase;
        }
        public async Task Handle(AddProductCommand command)
        {
            // Hacer el .Execute asyncoronomo y todo su interior
            await Task.Run(() => useCase.Execute(new AddProductRequest(command.ProductDTO)));
        }
    }
}
