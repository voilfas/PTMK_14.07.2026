using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.PositionDTOs;

namespace TicketService.Application.Features.Positions.Queries.GetPositions;

public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, PageResult<PositionDto>>
{
    private readonly IPositionReadRepository _repository;

    public GetPositionsHandler(IPositionReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<PositionDto>> Handle(
        GetPositionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
    }
}