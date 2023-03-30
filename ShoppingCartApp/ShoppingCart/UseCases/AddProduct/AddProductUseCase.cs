using ShoppingCartApp.Shared.UseCases;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Services.ShoppingCartAdministrator;

namespace ShoppingCartApp.App.UseCases.AddProduct
{
    public class AddProductUseCase : IBaseUseCase<AddProductRequest>
    {
        private IShoppingCartAdministratorService shoppingCartAdministrator;

        public AddProductUseCase(IShoppingCartAdministratorService shoppingCartAdministrator)
        {
            this.shoppingCartAdministrator = shoppingCartAdministrator;
        }

        public void Execute(AddProductRequest productRequest)
        {
            if (productRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(AddProductRequest)));

            Product product = new Product(productRequest.Name, productRequest.Price, productRequest.Quantity);
            ShoppingCart shoppingCart = new ShoppingCart(productRequest.ShoppingCartName, new List<Product>());
            
            shoppingCartAdministrator.AddProductToShoppingCart(shoppingCart, product);
        }
    }
}