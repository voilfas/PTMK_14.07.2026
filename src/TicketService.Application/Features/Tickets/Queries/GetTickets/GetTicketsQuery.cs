using MediatR;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetTickets;

public record GetTicketsQuery(TicketFilter Filter) : IRequest<PageResult<TicketListItemDto>>;