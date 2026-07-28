using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountCompletedTickets;

public class GetAmountCompletedTicketsHandler : IRequestHandler<GetAmountCompletedTicketsQuery, IReadOnlyCollection<TicketCompletedAmountExecutorDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountCompletedTicketsHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TicketCompletedAmountExecutorDto>> Handle(
        GetAmountCompletedTicketsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetCompletedAmountAsync(
            cancellationToken);
    }
}