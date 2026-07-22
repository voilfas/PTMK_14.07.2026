using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken =  default);
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}