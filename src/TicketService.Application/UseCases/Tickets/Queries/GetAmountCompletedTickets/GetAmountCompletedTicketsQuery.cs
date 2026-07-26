using MediatR;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountCompletedTickets;

public record GetAmountCompletedTicketsQuery() : 
    IRequest<IReadOnlyCollection<TicketCompletedAmountExecutorDto>>;