using ShoppingCartApp.App.Modules.ProductModule.UseCases.CheckStock;

namespace ShoppingCartApp.Shared.Domain
{
    public interface IEventBus
    {
        Task Publish(IReadOnlyCollection<IDomainEvent> domainEvents);
        void Subscribe<T>(IEventHandler<T> eventHandler) where T : IDomainEvent;
    }
}