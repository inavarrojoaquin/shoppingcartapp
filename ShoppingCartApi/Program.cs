using ShoppingCartApi.Decorators;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
using System.Net;
using ShoppingCartApp.Shared.Events;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ShoppingCartApp.Modules.ProductModule.UseCases.CheckStock;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.DeleteProduct;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart;
using PRODUCT_MODULE = ShoppingCartApp.Modules.ProductModule;
using SHOPCART_MODULE =  ShoppingCartApp.Modules.ShoppingCartModule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// DBContext
builder.Services.AddDbContext<SHOPCART_MODULE.Infrastructure.ShoppingCartDbContext>();
builder.Services.AddDbContext<PRODUCT_MODULE.Infrastructure.ProductDbContext>();

// Repositories
builder.Services.AddTransient<SHOPCART_MODULE.Infrastructure.IProductRepository, SHOPCART_MODULE.Infrastructure.ProductRepository>();
builder.Services.AddTransient<PRODUCT_MODULE.Infrastructure.IProductRepository, PRODUCT_MODULE.Infrastructure.ProductRepository>();
builder.Services.AddTransient<SHOPCART_MODULE.Infrastructure.IShoppingCartRepository, SHOPCART_MODULE.Infrastructure.ShoppingCartRepository>();

// UseCases
builder.Services.AddTransient<IBaseUseCase<AddProductRequest>, AddProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<DeleteProductRequest>, DeleteProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<PrintShoppingCartRequest, string>, PrintShoppingCartUseCase>();
builder.Services.AddTransient<IBaseUseCase<CloseShoppingCartRequest>, CloseShoppingCartUseCase>();
builder.Services.AddTransient<IBaseUseCase<CheckStockRequest>, CheckStockUseCase>();

// CQRS-Command
builder.Services.AddTransient<ICommandBus, InMemoryCommandBus>();
builder.Services.AddTransient<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<CloseShoppingCartCommand>, CloseShoppingCartCommandHandler>();

// CQRS-Queries
builder.Services.AddTransient<IQueryBus, InMemoryQueryBus>();
builder.Services.AddTransient<IQueryHandler<PrintShoppingCartQuery, string>, PrintShoppingCartQueryHandler>();

// Commands Decorator
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, CommandLoggingDecorator<AddProductRequest>>();
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, DbTransactionDecorator<AddProductRequest>>();

builder.Services.Decorate<IBaseUseCase<DeleteProductRequest>, CommandLoggingDecorator<DeleteProductRequest>>();
builder.Services.Decorate<IBaseUseCase<DeleteProductRequest>, DbTransactionDecorator<DeleteProductRequest>>();

// Queries Decorator
builder.Services.Decorate<IBaseUseCase<PrintShoppingCartRequest, string>, QueryLoggingDecorator<PrintShoppingCartRequest, string>>();

// Events
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddTransient<IEventHandler<ShoppingCartClosed>, CheckStockOnShoppingCartClosedHandler>();

//builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();

var app = builder.Build();

// Configure EventBus
var eventBusConfigurator = new BusConfigurator();
eventBusConfigurator.Subscribe(app);
eventBusConfigurator.StartCosumer(app);

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
public class BusConfigurator 
{ 
    public void Subscribe(IApplicationBuilder applicationBuilder)
    {
        var eventBus =  applicationBuilder.ApplicationServices.GetService<IEventBus>();
        var checkStockUseCase = applicationBuilder.ApplicationServices.GetService<IEventHandler<ShoppingCartClosed>>();

        eventBus.Subscribe<ShoppingCartClosed>((IEventHandler<ShoppingCartClosed>)checkStockUseCase);
    }

    public void StartCosumer(IApplicationBuilder applicationBuilder)
    {
        var eventBus = applicationBuilder.ApplicationServices.GetService<IEventBus>();
        ((RabbitMQEventBus)eventBus).StartConsuming("products.checkStatus_on_shoppingCart_closed", typeof(ShoppingCartClosed));
    }
}