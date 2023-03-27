using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public class ShoppingCartUseCase : IBaseUseCase<PrintShoppingCartRequest>
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;

        public ShoppingCartUseCase(IShoppingCartAdministrator shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(PrintShoppingCartRequest request)
        {
            if (request == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(PrintShoppingCartRequest)));

            ShoppingCart shoppingCart = new ShoppingCart(request.ShoppingCartName, new List<Product>());

            shoppingCartAdministrator.PrintShoppingCart(shoppingCart);
        }
    }
}