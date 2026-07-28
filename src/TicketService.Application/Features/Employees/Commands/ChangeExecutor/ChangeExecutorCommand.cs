using MediatR;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Employees.Commands.ChangeExecutor;

public record ChangeExecutorCommand(
    Guid TicketId,
    Guid OldExecutorId,
    Guid NewExecutorId) :  IRequest<Result>;