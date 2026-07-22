namespace TicketService.Application.UseCases.Employees.Commands.ChangeExecutor;

public record ChangeExecutorCommand(
    Guid TicketId,
    Guid OldExecutorId,
    Guid NewExecutorId);