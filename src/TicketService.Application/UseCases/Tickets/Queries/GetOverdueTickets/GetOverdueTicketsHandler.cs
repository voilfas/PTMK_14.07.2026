using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Tickets.Queries.GetOverdueTickets;

public class GetOverdueTicketsHandler : IRequestHandler<GetOverdueTicketsQuery, PageResult<TicketDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetOverdueTicketsHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<TicketDto>> Handle(
        GetOverdueTicketsQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetOverdueAsync(query.Page, query.PageSize, cancellationToken);
    }
}