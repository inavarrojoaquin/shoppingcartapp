using ShoppingCartApp.App.Domain;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartApp.App.UseCases.PrintShoppingCart
{
    public class ShoppingCartUseCase : IBaseUseCase<PrintShoppingCartRequest>
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;

        public ShoppingCartUseCase(IShoppingCartAdministratorService shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(PrintShoppingCartRequest request)
        {
            if (request == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(PrintShoppingCartRequest)));

            ShoppingCart shoppingCart = new ShoppingCart(request.ShoppingCartName, new List<OrderItem>());

            shoppingCartAdministrator.PrintShoppingCart(shoppingCart);
        }
    }
}