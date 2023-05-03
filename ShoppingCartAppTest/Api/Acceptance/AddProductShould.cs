using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAppTest.Api.Acceptance
{
    internal class AddProductShould
    {
        [Test]
        public async Task AddProductToShoppingCart()
        {
            IBaseUseCase<AddProductRequest> baseRequest = Substitute.For<IBaseUseCase<AddProductRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<AddProductRequest>>(x => baseRequest); }); 
            });

            var application2 = new WebApplicationFactory<Program>();

            var client = application2.CreateClient();
            var newProduct = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };

            var response = await client.PostAsJsonAsync("api/AddProduct", newProduct);

            baseRequest.Received().Execute(Arg.Any<AddProductRequest>());
        }
    }
}
