namespace Ticket.Domain.Enums;

public enum TicketStatus
{
    New = 0,
    AwaitingApproval = 1,
    Approved = 2,
    Rejected = 3,
    InProgress = 4,
    Completed = 5
}