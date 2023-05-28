using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.Close;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartAppTest.App.UseCases.Close;

public class CloseShoppingCartUseCaseShould
{
    private IShoppingCartRepository shoppingCartRepository;
    
    [SetUp]
    public void SetUp()
    {
        shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
    }
    
    [Test]
    public async Task CloseAShoppingCart()
    {
        IEventBus eventBus = Substitute.For<IEventBus>();
        Product product = new Product(ProductId.Create(), Name.Create(), ProductPrice.Create());
        var shoppingCartId = ShoppingCartId.Create();
        var shoppingCart = new ShoppingCart(shoppingCartId);
        shoppingCart.AddProduct(product);
        shoppingCartRepository.GetShoppingCartByIdAsync(Arg.Is<ShoppingCartId>(
            s => s.Value().Equals(shoppingCartId.Value()))).Returns(shoppingCart);
        var closeShoppingCartUseCase = new CloseShoppingCartUseCase(shoppingCartRepository, eventBus);
        
        await closeShoppingCartUseCase.ExecuteAsync(new CloseShoppingCartRequest { ShoppingCartId = shoppingCartId.Value() });

        await shoppingCartRepository.Received(1).SaveAsync(Arg.Is<ShoppingCart>(
            s => s.ToPrimitives().IsClosed == true));
    }
}