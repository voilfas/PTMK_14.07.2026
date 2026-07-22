using TicketService.Domain.Abstractions;

namespace TicketService.Domain.Events;

public class TicketExecutorAddListEvent : DomainEvent
{
    public Guid TicketId { get; }
    public IReadOnlyCollection<Guid>? ExecutorIds { get; }

    public TicketExecutorAddListEvent(Guid  ticketId, IReadOnlyCollection<Guid> executorIds)
    {
        TicketId = ticketId;
        ExecutorIds = executorIds;
    }
}