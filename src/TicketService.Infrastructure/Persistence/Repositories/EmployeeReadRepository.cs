using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;
using TicketService.Application.UseCases.Employees.Queries.GetEmployees;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class EmployeeReadRepository : IEmployeeReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PageResult<EmployeeDto>> GetAllAsync(EmployeeFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Employees.AsNoTracking();

        if (filter.DepartmentId is not null)
            query = query.Where(e => e.DepartmentId == filter.DepartmentId);

        if (filter.PositionId is not null)
            query = query.Where(e => e.PositionId == filter.PositionId);

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(e =>
                EF.Functions.ILike(e.FullName.FirstName, $"%{filter.Search}%") ||
                EF.Functions.ILike(e.FullName.LastName, $"%{filter.Search}%") ||
                EF.Functions.ILike(e.FullName.Surname, $"%{filter.Search}%"));
        }
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var employees = await query
            .OrderBy(e => e.FullName.LastName)
            .ThenBy(e => e.FullName.FirstName)
            .ThenBy(e => e.FullName.Surname)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(e => new EmployeeDto(
                e.FullName.FirstName,
                e.FullName.LastName,
                e.FullName.Surname,
                e.DepartmentId,
                e.PositionId))
            .ToListAsync(cancellationToken);
        
        return new PageResult<EmployeeDto>
        {
            Items = employees,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }
}