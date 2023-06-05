using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartUseCase : IBaseUseCase<CloseShoppingCartRequest>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IEventBus eventBus;

        public CloseShoppingCartUseCase(IShoppingCartRepository shoppingCartRepository, IEventBus eventBus)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.eventBus = eventBus;
        }

        public async Task ExecuteAsync(CloseShoppingCartRequest closeRequest)
        {
            if (closeRequest == null)
                throw new Exception(string.Format("Error: {0} can't be null", typeof(CloseShoppingCartRequest)));

            ShoppingCart shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(closeRequest.ShoppingCartId);

            if (shoppingCart == null)
                shoppingCart = new ShoppingCart(closeRequest.ShoppingCartId);

            shoppingCart.Close();

            await shoppingCartRepository.SaveAsync(shoppingCart);

            await eventBus.Publish(shoppingCart.GetEvents());
        }
    }
}