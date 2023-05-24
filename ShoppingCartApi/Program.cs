using ShoppingCartApi.Decorators;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.App.UseCases.PrintShoppingCart;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<ShoppingCartDbContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddTransient<IBaseUseCase<AddProductRequest>, AddProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<DeleteProductRequest>, DeleteProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<PrintShoppingCartRequest, string>, PrintShoppingCartUseCase>();

builder.Services.AddTransient<ICommandBus, InMemoryCommandBus>();
builder.Services.AddTransient<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();

builder.Services.AddTransient<IQueryBus, InMemoryQueryBus>();
builder.Services.AddTransient<IQueryHandler<PrintShoppingCartQuery, string>, PrintShoppingCartQueryHandler>();

// Adding generic decorator for logging
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, LoggingDecorator<AddProductRequest>>();
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, DbTransactionDecorator<AddProductRequest>>();

//Ver si se puede hacer generico con T o algo
//builder.Services.Decorate<IBaseUseCase<IBaseRequest>, LoggingDecorator<IBaseRequest>>();

var app = builder.Build();

app.UseExceptionHandler(handler => handler.Run(async context => 
    { 
        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
        context.Response.ContentType = "text/plain";
        
        await context.Response.WriteAsync("A ocurrido un error");
    }));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }