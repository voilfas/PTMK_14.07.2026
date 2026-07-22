namespace TicketService.Application.UseCases.Tickets.Commands.DeleteExecutor;

public record DeleteExecutorCommand(
    Guid TicketId,
    Guid ExecutorId);