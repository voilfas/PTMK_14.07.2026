using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.DepartmentDTOs;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IDepartmentReadRepository
{
    Task<PageResult<DepartmentDto>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}