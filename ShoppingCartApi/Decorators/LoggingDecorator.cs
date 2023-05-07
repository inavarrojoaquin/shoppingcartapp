using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators;

public class LoggingDecorator<T> : IBaseUseCase<T> where T : IBaseRequest  
{
    private readonly ILogger<T> logger;
    private readonly IBaseUseCase<T> useCase;

    public LoggingDecorator(ILogger<T> logger, IBaseUseCase<T> useCase)
    {
        this.logger = logger;
        this.useCase = useCase;
    }
    public void Execute(T request)
    {
        logger.LogInformation("Before executing " + useCase.GetType());
        useCase.Execute(request);
        logger.LogInformation("After executing " + useCase.GetType());
    }
}