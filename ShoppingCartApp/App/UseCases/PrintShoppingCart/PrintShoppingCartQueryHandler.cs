using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart;

public class PrintShoppingCartQueryHandler : IQueryHandler<PrintShoppingCartQuery, string>
{
    private readonly IBaseUseCase<PrintShoppingCartRequest, string> useCase;

    public PrintShoppingCartQueryHandler(IBaseUseCase<PrintShoppingCartRequest, string> useCase)
    {
        this.useCase = useCase;
    }
    public async Task<string> HandleAsync(PrintShoppingCartQuery query)
    {
        var printShoppingCartRequest = new PrintShoppingCartRequest(new ShoppingCartDTO
        {
            ShoppingCartId = query.ShoppingCartId
        });
        string output = null;
        await Task.Run(() => { output = useCase.Execute(printShoppingCartRequest); });
        return output;
    }
}