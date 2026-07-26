using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountOverdue;

public class GetAmountOverdueHandler : IRequestHandler<GetAmountOverdueQuery, int>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountOverdueHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        GetAmountOverdueQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAmountOverdueAsync(cancellationToken);
    }
}