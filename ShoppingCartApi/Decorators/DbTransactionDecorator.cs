using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Decorators
{
    public class DbTransactionDecorator<T> : IBaseUseCase<T> where T : IBaseRequest
    {
        private readonly ILogger<T> logger;
        private readonly IBaseUseCase<T> useCase;
        private readonly ShoppingCartDbContext context;

        public DbTransactionDecorator(ILogger<T> logger, IBaseUseCase<T> useCase, ShoppingCartDbContext context)
        {
            this.logger = logger;
            this.useCase = useCase;
            this.context = context;
        }

        public void Execute(T request)
        {
            logger.LogInformation("Start Transaction: " + request.GetType().ToString());

            context.StartTransaction();

            try
            {
                useCase.Execute(request);
                context.EndTransaction();

                logger.LogInformation("End Transaction: " + request.GetType().ToString());
            }
            catch (Exception)
            {
                context.RollbackTransaction();
                logger.LogInformation("Rollback Transaction: " + request.GetType().ToString());
                
                throw;
            }
        }
    }
}
