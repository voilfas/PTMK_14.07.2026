using MediatR;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTickets;

public record GetTicketsQuery(TicketFilter Filter) : IRequest<PageResult<TicketDto>>;