using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.Shared.UseCases;
using System.Net.Http.Json;

namespace ShoppingCartAppTest.Api.Acceptance
{
    internal class DeleteProductShould
    {
        [Test]
        public async Task DeleteProductFromShoppingCart()
        {
            IBaseUseCase<DeleteProductRequest> deleteProductRequest = Substitute.For<IBaseUseCase<DeleteProductRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<DeleteProductRequest>>(x => deleteProductRequest); }); 
            });

            var client = application.CreateClient();
            var newProduct = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };

            var response = await client.PostAsJsonAsync("api/DeleteProduct", newProduct);

            deleteProductRequest.Received(1).Execute(Arg.Is<DeleteProductRequest>(x => x.ProductId.Value() == newProduct.ProductId
                                                                                 && x.ShoppingCartId.Value() == newProduct.ShoppingCartId));
        }
    }
}
