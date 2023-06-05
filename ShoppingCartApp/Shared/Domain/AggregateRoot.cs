namespace ShoppingCartApp.Shared.Domain
{
    public class AggregateRoot<T> where T : IDomainEvent
    {
        private List<T> events = new List<T>();

        public void AddEvent(T domainEvent) 
        {
            events.Add(domainEvent);
        }

        public IReadOnlyCollection<T> GetEvents() 
        {
            return events.AsReadOnly();
        }
    }
}
