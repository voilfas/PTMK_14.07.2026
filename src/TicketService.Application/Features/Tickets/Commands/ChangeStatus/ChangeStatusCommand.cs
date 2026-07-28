using MediatR;
using TicketService.Domain.Common;
using TicketService.Domain.Enums;

namespace TicketService.Application.Features.Tickets.Commands.ChangeStatus;

public record ChangeStatusCommand(
    Guid Id,
    TicketStatus Status
    ) :  IRequest<Result>;