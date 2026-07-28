using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
    
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    
    Task DeleteAsync(Ticket ticket, CancellationToken cancellationToken);
}