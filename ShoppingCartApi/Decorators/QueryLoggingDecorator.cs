using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators
{
    public class QueryLoggingDecorator<T, TResult> : IBaseUseCase<T, TResult> where T : IBaseRequest
    {
        private readonly ILogger<T> logger;
        private readonly IBaseUseCase<T, TResult> useCase;

        public QueryLoggingDecorator(ILogger<T> logger, IBaseUseCase<T, TResult> useCase)
        {
            this.logger = logger;
            this.useCase = useCase;
        }

        public async Task<TResult> ExecuteAsync(T request)
        {
            logger.LogInformation(string.Format("Start Query-Logging: {0}", request.GetType().Name));

            var result = await useCase.ExecuteAsync(request);

            logger.LogInformation(string.Format("End Query-Logging: {0} \n\tResult: {1}", request.GetType().Name, result));

            return result;
        }
    }
}
