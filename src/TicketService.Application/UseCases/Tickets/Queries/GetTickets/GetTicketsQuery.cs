using MediatR;
using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTickets;

public record GetTicketsQuery(TicketFilter Filter) : IRequest<PageResult<TicketListItemDto>>;