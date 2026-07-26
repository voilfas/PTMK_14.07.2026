using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.ResponceDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountByStatus;

public class GetAmountByStatusHandler 
    : IRequestHandler<GetAmountByStatusRequest, 
        IReadOnlyCollection<TicketStatusReportDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetAmountByStatusHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<TicketStatusReportDto>> Handle(
        GetAmountByStatusRequest request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetStatusReportAsync(cancellationToken);
    }
}