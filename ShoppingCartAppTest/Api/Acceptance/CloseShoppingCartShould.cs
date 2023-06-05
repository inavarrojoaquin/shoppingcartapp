using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ShoppingCartApp.App.Modules.ShoppingCartModule.UseCases.CloseShoppingCart;
using ShoppingCartApp.Shared.UseCases;
using System.Net.Http.Json;

namespace ShoppingCartAppTest.Api.Acceptance
{
    internal class CloseShoppingCartShould
    {
        [Test]
        public async Task CloseShoppingCart()
        {
            IBaseUseCase<CloseShoppingCartRequest> closeShoppingCartUseCase = Substitute.For<IBaseUseCase<CloseShoppingCartRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<CloseShoppingCartRequest>>(x => closeShoppingCartUseCase); });
            });

            var client = application.CreateClient();

            var closeShoppingCartDto = new { ShoppingCartId = Guid.NewGuid().ToString() };

            var response = await client.PostAsJsonAsync("api/ShoppingCart/close", closeShoppingCartDto);

            await closeShoppingCartUseCase.Received(1).ExecuteAsync(Arg.Is<CloseShoppingCartRequest>(x => x.ShoppingCartId.Value() == closeShoppingCartDto.ShoppingCartId));
        }
    }
}
