using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IPositionReadRepository
{
    Task<PageResult<PositionDto>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}