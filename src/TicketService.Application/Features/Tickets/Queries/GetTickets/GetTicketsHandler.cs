using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetTickets;

public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, PageResult<TicketListItemDto>>
{
    private readonly ITicketReadRepository _ticketReadRepository;

    public GetTicketsHandler(ITicketReadRepository ticketReadRepository)
    {
        _ticketReadRepository = ticketReadRepository;
    }

    public async Task<PageResult<TicketListItemDto>> Handle(
        GetTicketsQuery request,
        CancellationToken cancellationToken)
    {
        return await _ticketReadRepository.GetAllAsync(request.Filter, cancellationToken);
    }
}