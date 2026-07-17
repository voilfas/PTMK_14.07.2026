using Ticket.Domain.Abstractions;
using Ticket.Domain.Enums;

namespace Ticket.Domain.Events;

public class TicketStatusChangedEvent : DomainEvent
{
    public Guid TicketId { get; }
    public TicketStatus OldStatus { get; }
    public TicketStatus NewStatus { get; }

    public TicketStatusChangedEvent(Guid ticketId, TicketStatus oldStatus, TicketStatus newStatus)
    {
        TicketId = ticketId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }
}