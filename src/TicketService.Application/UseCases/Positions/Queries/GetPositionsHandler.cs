using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Positions.Queries;

public class GetPositionsHandler
{
    private readonly IPositionReadRepository _repository;

    public GetPositionsHandler(IPositionReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<PositionDto>> Handle(
        GetPositionsQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(query.Page, query.PageSize, cancellationToken);
    }
}