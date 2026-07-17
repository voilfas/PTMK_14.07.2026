using Ticket.Domain.Abstractions;

namespace Ticket.Domain.Events;

public class TicketExecutorAddListEvent : DomainEvent
{
    public Guid TicketId { get; }
    public List<Guid>? ExecutorIds { get; }

    public TicketExecutorAddListEvent(Guid  ticketId, List<Guid>? executorIds)
    {
        TicketId = ticketId;
        ExecutorIds = executorIds;
    }
}