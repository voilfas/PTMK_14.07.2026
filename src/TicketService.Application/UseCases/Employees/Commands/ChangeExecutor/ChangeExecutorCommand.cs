using MediatR;
using TicketService.Application.Common;
using TicketService.Domain.Common;
using TicketService.Domain.Entities;

namespace TicketService.Application.UseCases.Employees.Commands.ChangeExecutor;

public record ChangeExecutorCommand(
    Guid TicketId,
    Guid OldExecutorId,
    Guid NewExecutorId) :  IRequest<Result>;