using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.DeleteProduct
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IBaseUseCase<DeleteProductRequest> useCase;

        public DeleteProductCommandHandler(IBaseUseCase<DeleteProductRequest> useCase)
        {
            this.useCase = useCase;
        }
        public async Task Handle(DeleteProductCommand command)
        {
            await useCase.ExecuteAsync(new DeleteProductRequest(command.ProductDTO));
        }
    }
}
