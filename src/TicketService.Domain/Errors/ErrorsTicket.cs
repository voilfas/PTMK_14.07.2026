using TicketService.Domain.Common;

namespace TicketService.Domain.Errors;

public static class ErrorsTicket
{
    public static readonly Error EmptyAuthorId = new Error("Ticket.EmptyAuthorId", "Id of author can't be empty");
    public static readonly Error EmptyExecutorId = new Error("Ticket.EmptyExecutorId", "Id of executor is present but it's empty");
    
    public static readonly Error EmptyDescription = new Error("Ticket.EmptyDescription", "Description can't be empty");
    public static readonly Error IncorrectDescription = new Error("Ticket.IncorrectDescription", "Description should contain from 3 - 300 symbols");

    public static readonly Error InvalidStatusTransition = new Error("Ticket.InvalidStatusTransition", "This transition is invalid");

    public static readonly Error CompletedOrRejectedTicketStatus = new Error("Ticket.CompletedOrRejectedTicketStatus", "Can't add executor for ticket with status completed or rejected");
    public static readonly Error DistinctExecutors = new Error("Ticket.DistinctExecutors", "Can't add same executors");
    
    public static readonly Error ExecutorDoesNotExist = new Error("Ticket.ExecutorDoesNotExist", "Executor with this id does not exist");
    public static readonly Error ExecutorAlreadyExist = new Error("Ticket.ExecutorAlreadyExist", "Executor with this id already exist in ticket");
    public static readonly Error CannotChangeExecutorsInCurrentStatus = new Error("Ticket.CannotChangeExecutorsInCurrentStatus", "Can't change executors in current status");
}