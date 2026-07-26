using MediatR;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountOverdue;

public sealed record GetAmountOverdueQuery() : IRequest<int>;