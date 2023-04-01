using ShoppingCartApp.App.Domain;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartApp.App.UseCases.DeleteProduct
{
    public class DeleteProductUseCase : IBaseUseCase<DeleteProductRequest>
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;

        public DeleteProductUseCase(IShoppingCartAdministratorService shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(DeleteProductRequest productRequest)
        {
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest)));

            Product product = new Product(productRequest.Name);
            OrderItem orderItem = new OrderItem(product);
            ShoppingCart shoppingCart = new ShoppingCart(productRequest.ShoppingCartName, new List<OrderItem>());

            shoppingCartAdministrator.DeleteProductFromShoppingCart(shoppingCart, orderItem);
        }
    }
}