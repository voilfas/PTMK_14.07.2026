using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.PositionDTOs;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IPositionReadRepository
{
    Task<PageResult<PositionDto>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}