using MediatR;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetRecentTickets;

public sealed record GetRecentTicketsQuery() : 
    IRequest<IReadOnlyCollection<TicketListItemDto>>;