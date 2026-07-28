using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountByStatus;

public class GetAmountByStatusHandler 
    : IRequestHandler<GetAmountByStatusQuery, 
        IReadOnlyCollection<TicketStatusReportDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountByStatusHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TicketStatusReportDto>> Handle(
        GetAmountByStatusQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetStatusReportAsync(cancellationToken);
    }
}