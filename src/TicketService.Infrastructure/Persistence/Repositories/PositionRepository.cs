using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public PositionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Position position, CancellationToken cancellationToken)
    {
        await _dbContext.Positions.AddAsync(position, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Positions.AnyAsync(p => p.Id == id, cancellationToken);
    }
}