using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public class DeleteProductUseCase : IBaseUseCase<DeleteProductRequest>
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;

        public DeleteProductUseCase(IShoppingCartAdministrator shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(DeleteProductRequest productRequest)
        {
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(DeleteProductRequest)));

            Product product = new Product(productRequest.Name);
            ShoppingCart shoppingCart = new ShoppingCart(productRequest.ShoppingCartName);

            shoppingCartAdministrator.DeleteProductFromShoppingCart(shoppingCart, product);
        }
    }
}