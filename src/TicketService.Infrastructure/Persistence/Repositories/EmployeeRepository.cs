using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await _dbContext.Employees.AddAsync(employee, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> AllExistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var distinctIds = ids.Distinct().ToList();
        
        if (!distinctIds.Any())
            return true;
        
        var existsCount = await _dbContext.Employees.CountAsync(e => distinctIds.Contains(e.Id), cancellationToken);

        return distinctIds.Count == existsCount;
    }
}