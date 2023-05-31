using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartQueryHandler : IQueryHandler<PrintShoppingCartQuery, string>
    {
        private readonly IBaseUseCase<PrintShoppingCartRequest, string> useCase;

        public PrintShoppingCartQueryHandler(IBaseUseCase<PrintShoppingCartRequest, string> useCase)
        {
            this.useCase = useCase;
        }
        public async Task<string> Handle(PrintShoppingCartQuery query)
        {
            return await useCase.ExecuteAsync(new PrintShoppingCartRequest(new DTOs.ShoppingCartDTO { ShoppingCartId = query.ShoppingCartId }));
        }
    }
}
