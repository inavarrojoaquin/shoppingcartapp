namespace ShoppingCartApp.Shared.Domain;

public class AggregateRoot
{
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public IReadOnlyCollection<IDomainEvent> GetEvents()
    {
        return _events.AsReadOnly();
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}