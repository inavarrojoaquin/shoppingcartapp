using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.Close;

public class CloseShoppingCartCommandHandler : ICommandHandler<CloseShoppingCartCommand>
{
    private readonly IBaseUseCase<CloseShoppingCartRequest> useCase;

    public CloseShoppingCartCommandHandler(IBaseUseCase<CloseShoppingCartRequest> useCase)
    {
        this.useCase = useCase;
    }
    public Task Handle(CloseShoppingCartCommand command)
    {
        var closeShoppingCartRequest = new CloseShoppingCartRequest
        {
            ShoppingCartId = command.ShoppingCartId
        };
        return useCase.ExecuteAsync(closeShoppingCartRequest);
    }
}