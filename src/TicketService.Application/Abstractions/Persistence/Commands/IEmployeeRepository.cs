using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface IEmployeeRepository
{
    Task AddAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AllExistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}