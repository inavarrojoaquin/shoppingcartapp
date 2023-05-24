﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ShoppingCartApp.App.Domain;
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
            IBaseUseCase<AddProductRequest> addProductRequest = Substitute.For<IBaseUseCase<AddProductRequest>>();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => { services.AddTransient<IBaseUseCase<AddProductRequest>>(x => addProductRequest); }); 
            });

            var client = application.CreateClient();
            var newProduct = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };

            var response = await client.PostAsJsonAsync("api/AddProduct", newProduct);

            addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct.ProductId
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
            var response1 = await client.PostAsJsonAsync("api/AddProduct", newProduct1);

            var newProduct2 = new { ProductId = Guid.NewGuid().ToString(), ShoppingCartId = Guid.NewGuid().ToString() };
            var response2 = await client.PostAsJsonAsync("api/AddProduct", newProduct2);

            addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct1.ProductId
                                                                                 && x.ShoppingCartId.Value() == newProduct1.ShoppingCartId));

            addProductRequest.Received(1).ExecuteAsync(Arg.Is<AddProductRequest>(x => x.ProductId.Value() == newProduct2.ProductId
                                                                                 && x.ShoppingCartId.Value() == newProduct2.ShoppingCartId));

        }
    }
}
