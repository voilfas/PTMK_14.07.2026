using MediatR;
using TicketService.Domain.Common;
using TicketService.Domain.Enums;

namespace TicketService.Application.UseCases.Tickets.Commands.CreateTicket;

public record CreateTicketCommand(
    Guid AuthorId,
    string Description,
    TicketType TicketType,
    IReadOnlyCollection<Guid> ExecutorIds
    ) :  IRequest<Result<Guid>>;