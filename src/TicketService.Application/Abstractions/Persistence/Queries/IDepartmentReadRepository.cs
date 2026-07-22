using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IDepartmentReadRepository
{
    Task<PageResult<DepartmentDto>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}