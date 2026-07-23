using MediatR;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetOverdueTickets;

public record GetOverdueTicketsQuery() : PageQuery, IRequest<PageResult<TicketDto>>;