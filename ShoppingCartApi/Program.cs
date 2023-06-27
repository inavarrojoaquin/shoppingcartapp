using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingCartApi.Decorators;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.AddProduct;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CheckProductStock;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.CloseShoppingCart;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.DeleteProduct;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.PrintShoppingCart;
using ShoppingCartApp.Modules.ShoppingCartModule.UseCases.UpdateProductStock;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;
using ShoppingCartApp.Shared.Infrastructure;
using ShoppingCartApp.Shared.UseCases;
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

// DBContext
builder.Services.AddDbContext<ShoppingCartDbContext>();
//builder.Services.AddDbContext<ProductDbContext>();

// Repositories
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddTransient<ISMProductRepository, SMProductRepository>();
//builder.Services.AddTransient<IPMProductRepository, PMProductRepository>();


// UseCases
builder.Services.AddTransient<IBaseUseCase<AddProductRequest>, AddProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<DeleteProductRequest>, DeleteProductUseCase>();
builder.Services.AddTransient<IBaseUseCase<PrintShoppingCartRequest, string>, PrintShoppingCartUseCase>();
builder.Services.AddTransient<IBaseUseCase<CloseShoppingCartRequest>, CloseShoppingCartUseCase>();
builder.Services.AddTransient<IBaseUseCase<CheckStockRequest>, CheckStockUseCase>();
builder.Services.AddTransient<IBaseUseCase<UpdatetSockRequest>, UpdateStockUseCase>();

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

// Event Bus Service -> The last in order is going to be the one ejecuting de eventBus.Publish
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();

// Events
builder.Services.AddTransient<IEventHandler<ShoppingCartClosed>, CheckStockOnShoppingCartClosedHandler>();
builder.Services.AddTransient<IEventHandler<StockUpdated>, UpdateStockOnStockUpdatedHandler>();

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
        var scope = applicationBuilder.ApplicationServices.CreateScope();
        var eventBuses = scope.ServiceProvider.GetServices<IEventBus>();
        var shoppingCartClosedHandler = scope.ServiceProvider.GetService<IEventHandler<ShoppingCartClosed>>();
        var stockUpdatedHandler = scope.ServiceProvider.GetService<IEventHandler<StockUpdated>>();

        foreach (var eventBus in eventBuses)
        {
            eventBus.Subscribe(shoppingCartClosedHandler);
            eventBus.Subscribe(stockUpdatedHandler);
        }
    }

    public void StartCosumer(IApplicationBuilder applicationBuilder)
    {
        var eventBuses = applicationBuilder.ApplicationServices.GetServices<IEventBus>();
        
        foreach(var eventBus in eventBuses)
        {
            if(eventBus.GetType() == typeof(RabbitMQEventBus))
            {
                ((RabbitMQEventBus)eventBus).StartConsuming("products.checkStock_on_shoppingCartClosed", typeof(ShoppingCartClosed));
                ((RabbitMQEventBus)eventBus).StartConsuming("products.updatedStock_on_stockUpdated", typeof(StockUpdated));
            }
        }
    }
}