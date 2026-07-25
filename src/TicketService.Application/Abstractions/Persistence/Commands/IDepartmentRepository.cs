using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    
    Task<CodeDepartment?> GetLastCodeAsync(CancellationToken cancellationToken);
}