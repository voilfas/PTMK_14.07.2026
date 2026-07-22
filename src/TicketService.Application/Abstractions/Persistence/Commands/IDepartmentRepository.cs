using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}