using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;
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

        public async Task ExecuteAsync(T request)
        {
            logger.LogInformation("Start Transaction: " + request.GetType().Name);

            await context.StartTransactionAsync();

            try
            {
                await useCase.ExecuteAsync(request);
                await context.EndTransactionAsync();

                logger.LogInformation("End Transaction: " + request.GetType().Name);
            }
            catch (Exception)
            {
                await context.RollbackTransactionAsync();
                logger.LogInformation("Rollback Transaction: " + request.GetType().Name);
                
                throw;
            }
        }
    }
}
