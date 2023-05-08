using Microsoft.Extensions.Logging;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators
{
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
            logger.LogInformation("Start Logging: " + request.GetType().ToString());

            useCase.Execute(request);

            logger.LogInformation("End Logging: " + request.GetType().ToString());
        }
    }
}
