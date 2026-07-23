using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class DepartmentReadRepository : IDepartmentReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PageResult<DepartmentDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Departments.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var departments = await query
            .OrderBy(d => d.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DepartmentDto(
                d.Name,
                d.Code.Code,
                d.IsActive))
            .ToListAsync(cancellationToken);
        
        return new PageResult<DepartmentDto>
        {
            Items = departments,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}