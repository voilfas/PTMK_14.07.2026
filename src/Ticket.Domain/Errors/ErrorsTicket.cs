using Ticket.Domain.Common;

namespace Ticket.Domain.Errors;

public static class ErrorsTicket
{
    public static readonly Error EmptyAuthorId = new Error("Ticket.EmptyAuthorId", "Id of author can't be empty");
    public static readonly Error EmptyExecutorId = new Error("Ticket.EmptyExecutorId", "Id of executor can't be empty");
    
    public static readonly Error EmptyDescription = new Error("Ticket.EmptyDescription", "Description can't be empty");
    
    public static readonly Error EmptyDeadLine = new Error("Ticket.EmptyDeadLine", "DeadLine can't be empty");
    public static readonly Error IncorrectDeadLine = new Error("Ticket.IncorrectDeadLine", "DeadLine can't be earlier than the current day and later than 1 year");
}