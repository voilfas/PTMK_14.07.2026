using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IDepartmentReadRepository
{
    Task<PageResult<DepartmentDto>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}