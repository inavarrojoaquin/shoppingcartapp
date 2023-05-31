using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.App.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartUseCase : IBaseUseCase<CloseShoppingCartRequest>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        //public CloseShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository, IEventBus eventBus)
        //{
        //    this.shoppingCartRepository = shoppingCartRepository;
        //}

        public async Task ExecuteAsync(CloseShoppingCartRequest closeRequest)
        {
            if (closeRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(CloseShoppingCartRequest)));

            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(closeRequest.ShoppingCartId);

            if (shoppingCart == null)
                shoppingCart = new ShoppingCart(closeRequest.ShoppingCartId);

            shoppingCart.Close();

            //await shoppingCartRepository.SaveAsync(shoppingCart);

            //eventBus.Publish(shoppingCart.GetEvents());

            //use case nuevo  
        }
    }
}