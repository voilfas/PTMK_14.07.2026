using MediatR;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountOverdue;

public sealed record GetAmountOverdueQuery() : IRequest<int>;