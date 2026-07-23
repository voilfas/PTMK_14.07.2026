using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        await _dbContext.Departments.AddAsync(department, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Departments.AnyAsync(e => e.Id == id, cancellationToken);
    }
}