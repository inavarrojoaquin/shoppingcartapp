using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct;
using ShoppingCartApp.Shared.UseCases;
using System.Net.Http.Json;

namespace ShoppingCartAppTest.Api.Acceptance
{
    internal class AddProductShould
    {
        [Test]
        public async Task AddProductToShoppingCart()
        {
            IBaseUseCase<AddProductRequest> addProductRequest = Substitute.For<IBaseUseCase<AddProductRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<AddProductRequest>>(x => addProductRequest); });
            });

            var client = application.CreateClient();
            var newProduct = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };

            var response = await client.PostAsJsonAsync("api/ShoppingCart/product/add", newProduct);

            await addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct.ProductId
                                                                                 && x.ShoppingCartId.Value() == newProduct.ShoppingCartId));
        }

        [Test]
        public async Task AddTwoProductsToShoppingCart()
        {
            IBaseUseCase<AddProductRequest> addProductRequest = Substitute.For<IBaseUseCase<AddProductRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<AddProductRequest>>(x => addProductRequest); });
            });

            var client = application.CreateClient();

            var newProduct1 = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };
            var response1 = await client.PostAsJsonAsync("api/ShoppingCart/product/add", newProduct1);

            var newProduct2 = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };
            var response2 = await client.PostAsJsonAsync("api/ShoppingCart/product/add", newProduct2);

            await addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct1.ProductId
                && x.ShoppingCartId.Value() == newProduct1.ShoppingCartId));

            await addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct2.ProductId
                && x.ShoppingCartId.Value() == newProduct2.ShoppingCartId));

        }
    }
}
