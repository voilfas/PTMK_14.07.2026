using MediatR;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountCompletedTickets;

public record GetAmountCompletedTicketsQuery() : 
    IRequest<IReadOnlyCollection<TicketCompletedAmountExecutorDto>>;