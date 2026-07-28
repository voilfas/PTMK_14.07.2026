using MediatR;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.DeleteTicketById;

public record DeleteTicketByIdCommand(Guid Id) : IRequest<Result>;