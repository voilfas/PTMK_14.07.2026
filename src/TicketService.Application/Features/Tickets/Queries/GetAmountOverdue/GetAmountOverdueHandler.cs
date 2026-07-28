using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountOverdue;

public class GetAmountOverdueHandler : IRequestHandler<GetAmountOverdueQuery, int>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountOverdueHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        GetAmountOverdueQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAmountOverdueAsync(cancellationToken);
    }
}