using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators
{
    public class CommandLoggingDecorator<T> : IBaseUseCase<T> where T : IBaseRequest
    {
        private readonly ILogger<T> logger;
        private readonly IBaseUseCase<T> useCase;

        public CommandLoggingDecorator(ILogger<T> logger, IBaseUseCase<T> useCase)
        {
            this.logger = logger;
            this.useCase = useCase;
        }
        public async Task ExecuteAsync(T request)
        {
            logger.LogInformation("Start Command-Logging: " + request.GetType().Name);

            await useCase.ExecuteAsync(request);

            logger.LogInformation("End Command-Logging: " + request.GetType().Name);
        }
    }
}
