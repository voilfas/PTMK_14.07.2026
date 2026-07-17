namespace TicketService.Domain.Abstractions;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _events = [];
    public IReadOnlyList<DomainEvent> Events => _events.AsReadOnly();
    
    protected void AddEvent(DomainEvent @event) => _events.Add(@event);
    
    public void ClearEvents() => _events.Clear();
}