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
using System.Runtime.CompilerServices;
using ShoppingCartApp.App.Modules.Product.UseCases;
using ShoppingCartApp.App.UseCases.Close;
using ShoppingCartApp.Shared.Events;

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
builder.Services.AddTransient<IBaseUseCase<CloseShoppingCartRequest>, CloseShoppingCartUseCase>();

builder.Services.AddTransient<ICommandBus, InMemoryCommandBus>();
builder.Services.AddTransient<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<CloseShoppingCartCommand>, CloseShoppingCartCommandHandler>();

builder.Services.AddTransient<IQueryBus, InMemoryQueryBus>();
builder.Services.AddTransient<IQueryHandler<PrintShoppingCartQuery, string>, PrintShoppingCartQueryHandler>();

// Commands Decorator
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, CommandLoggingDecorator<AddProductRequest>>();
builder.Services.Decorate<IBaseUseCase<AddProductRequest>, DbTransactionDecorator<AddProductRequest>>();

builder.Services.Decorate<IBaseUseCase<DeleteProductRequest>, CommandLoggingDecorator<DeleteProductRequest>>();
builder.Services.Decorate<IBaseUseCase<DeleteProductRequest>, DbTransactionDecorator<DeleteProductRequest>>();

// Queries Decorator
builder.Services.Decorate<IBaseUseCase<PrintShoppingCartRequest, string>, QueryLoggingDecorator<PrintShoppingCartRequest, string>>();

var servicesConfigurator = new ServicesConfigurator();
servicesConfigurator.ConfigureEventBus(builder.Services);

var app = builder.Build();
servicesConfigurator.Configure(app);

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

public partial class Program
{
}

public class ServicesConfigurator
{
    public void ConfigureEventBus(IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton<CheckStockUseCase>();
    }
    
    public void Configure(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        var checkStockUseCase = app.ApplicationServices.GetRequiredService<CheckStockUseCase>();
        eventBus.Subscribe<CloseShoppingCartEvent>(checkStockUseCase.Handle);
    }
}