using System.ComponentModel.DataAnnotations.Schema;
using TicketService.Domain.Entities;

namespace TicketService.Domain.Abstractions;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<DomainEvent> _events = [];
    [NotMapped]
    public IReadOnlyList<DomainEvent> Events => _events.AsReadOnly();
    
    protected void AddEvent(DomainEvent @event) 
        => _events.Add(@event);

    public IReadOnlyList<DomainEvent> ClearEvents()
    {
        var events = _events;
        _events.Clear();
        return events;
    }
}