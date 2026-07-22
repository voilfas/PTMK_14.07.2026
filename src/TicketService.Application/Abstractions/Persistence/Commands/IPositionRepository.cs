using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Commands;

public interface IPositionRepository
{
    Task AddAsync(Position position, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}