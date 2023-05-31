namespace ShoppingCartApp.Shared.Domain
{
    public class AggregateRoot
    {
        private List<IDomainEvent> events = new List<IDomainEvent>();

        public void AddEvent(IDomainEvent domainEvent) 
        {
            events.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent> GetEvents()
        {
            return events.AsReadOnly();
        }
    }
}
