using TicketService.Domain.Abstractions;

namespace TicketService.Domain.Events;

public class TicketExecutorRemoveEvent : DomainEvent
{
    public Guid TicketId { get; }
    public Guid ExecutorId { get; }

    public TicketExecutorRemoveEvent(Guid ticketId, Guid executorId)
    {
        TicketId = ticketId;
        ExecutorId = executorId;
    }
}