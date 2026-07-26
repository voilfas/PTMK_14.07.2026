using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountCompletedTickets;

public class GetAmountCompletedTicketsHandler : IRequestHandler<GetAmountCompletedTicketsQuery, IReadOnlyCollection<TicketCompletedAmountExecutorDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountCompletedTicketsHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TicketCompletedAmountExecutorDto>> Handle(
        GetAmountCompletedTicketsQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetCompletedAmountAsync(
            cancellationToken);
    }
}