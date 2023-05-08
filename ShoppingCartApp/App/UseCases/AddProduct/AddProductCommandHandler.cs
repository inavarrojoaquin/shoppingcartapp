using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.AddProduct;

public class AddProductCommandHandler  : ICommandHandler<AddProductCommand>
{
    private readonly IBaseUseCase<AddProductRequest> useCase;

    public AddProductCommandHandler(IBaseUseCase<AddProductRequest> useCase)
    {
        this.useCase = useCase;
    }
    public async Task Handle(AddProductCommand command)
    {
        var addProductRequest = new AddProductRequest(command.productDTO);
        await Task.Run(() =>
        {
            useCase.Execute(addProductRequest);
        });
    }
}