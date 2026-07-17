using TicketService.Domain.Abstractions;

namespace TicketService.Domain.Events;

public class TicketExecutorChangedEvent : DomainEvent
{
    public Guid TicketId { get; }
    public Guid OldExecutorId { get; }
    public Guid NewExecutorId { get; }

    public TicketExecutorChangedEvent(Guid  ticketId, Guid oldExecutorId, Guid newExecutorId)
    {
        TicketId = ticketId;
        OldExecutorId = oldExecutorId;
        NewExecutorId = newExecutorId;
    }
}