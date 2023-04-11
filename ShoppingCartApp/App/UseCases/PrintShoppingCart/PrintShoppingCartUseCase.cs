using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart
{
    public class PrintShoppingCartUseCase : IBaseUseCase<PrintShoppingCartRequest>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public PrintShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void Execute(PrintShoppingCartRequest request)
        {
            if (request == null)
                throw new Exception(string.Format("Error: {0} can't be null", nameof(PrintShoppingCartRequest)));

            ShoppingCart shoppingCart = shoppingCartRepository.GetShoppingCartById(request.ShoppingCartId);
            
            string result = shoppingCart.Print();
        }
    }
}