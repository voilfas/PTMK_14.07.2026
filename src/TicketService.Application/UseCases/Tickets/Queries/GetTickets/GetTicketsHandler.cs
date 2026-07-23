using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTickets;

public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, PageResult<TicketDto>>
{
    private readonly ITicketReadRepository _ticketReadRepository;

    public GetTicketsHandler(ITicketReadRepository ticketReadRepository)
    {
        _ticketReadRepository = ticketReadRepository;
    }

    public async Task<PageResult<TicketDto>> Handle(
        GetTicketsQuery query,
        CancellationToken cancellationToken)
    {
        return await _ticketReadRepository.GetAllAsync(query.Filter, cancellationToken);
    }
}