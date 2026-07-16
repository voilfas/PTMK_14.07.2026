using Ticket.Domain.Common;

namespace Ticket.Domain.Errors;

public static class ErrorsTicket
{
    public static readonly Error EmptyAuthorId = new Error("Ticket.EmptyAuthorId", "Id of author can't be empty");
    public static readonly Error EmptyExecutorId = new Error("Ticket.EmptyExecutorId", "Id of executor is present but it's empty");
    
    public static readonly Error EmptyDescription = new Error("Ticket.EmptyDescription", "Description can't be empty");
    public static readonly Error IncorrectDescription = new Error("Ticket.IncorrectDescription", "Description should contain from 3 - 300 symbols");
    
    public static readonly Error EmptyDeadLine = new Error("Ticket.EmptyDeadLine", "DeadLine can't be empty");
    public static readonly Error IncorrectDeadLine = new Error("Ticket.IncorrectDeadLine", "DeadLine can't be earlier than the current day and later than 1 year");
    
    public static readonly Error FailStatusFromNewToCompleted = new Error("Ticket.FailStatusFromNewToCompleted", "Can't change status from new to completed");
    public static readonly Error FailStatusFromCompletedToInProgress = new Error("Ticket.FailStatusFromCompletedToInProgress", "Can't change status from completed to in progress");
    public static readonly Error InvalidStatusTransition = new Error("Ticket.InvalidStatusTransition", "This transition is invalid");
    
    public static readonly Error InvalidChangeExecutor = new Error("Ticket.InvalidChangeExecutor", "Can't change the executor when ticket is completed");
}