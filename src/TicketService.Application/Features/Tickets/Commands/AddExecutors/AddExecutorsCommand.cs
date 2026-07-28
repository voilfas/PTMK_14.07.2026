using MediatR;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.AddExecutors;

public record AddExecutorsCommand(
    Guid TicketId,
    IReadOnlyCollection<Guid> ExecutorsIds
    ) : IRequest<Result>;