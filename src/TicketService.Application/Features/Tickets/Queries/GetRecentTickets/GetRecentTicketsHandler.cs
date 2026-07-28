using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetRecentTickets;

public class GetRecentTicketsHandler : IRequestHandler<GetRecentTicketsQuery, IReadOnlyCollection<TicketListItemDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetRecentTicketsHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TicketListItemDto>> Handle(GetRecentTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRecentAsync(cancellationToken);
    }
}