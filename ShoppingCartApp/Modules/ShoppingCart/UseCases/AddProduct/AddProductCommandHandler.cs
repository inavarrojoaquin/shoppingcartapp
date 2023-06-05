using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct
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
            await useCase.ExecuteAsync(new AddProductRequest(command.ProductDTO));
        }
    }
}
