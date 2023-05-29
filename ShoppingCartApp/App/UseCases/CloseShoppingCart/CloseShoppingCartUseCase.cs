using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartUseCase : IBaseUseCase<CloseShoppingCartRequest>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public CloseShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task ExecuteAsync(CloseShoppingCartRequest closeRequest)
        {
            if (closeRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(CloseShoppingCartRequest)));

            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(closeRequest.ShoppingCartId);
            
            if(shoppingCart == null)
                shoppingCart = new ShoppingCart(closeRequest.ShoppingCartId);
            
            shoppingCart.Close();

            await shoppingCartRepository.SaveAsync(shoppingCart);
        }
    }
}