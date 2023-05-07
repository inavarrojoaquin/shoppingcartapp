using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators;

public class DatabaseDecorator<T> : IBaseUseCase<T> where T : IBaseRequest
{
    private readonly ShoppingCartDbContext dbContext;
    private readonly IBaseUseCase<T> useCase;
    private readonly ILogger<T> logger;

    public DatabaseDecorator(ShoppingCartDbContext dbContext, IBaseUseCase<T> useCase, ILogger<T> logger)
    {
        this.dbContext = dbContext;
        this.useCase = useCase;
        this.logger = logger;
    }
    public void Execute(T request)
    {
        try
        {
            logger.LogInformation("Starting transaction");
            dbContext.BeginTransaction();
            useCase.Execute(request);
            dbContext.CommitTransaction();
            logger.LogInformation("Commiting transaction");
        }
        catch (Exception)
        {
            logger.LogInformation("Rolling back transaction");
            dbContext.RollbackTransaction();
            throw;
        }
    }
}