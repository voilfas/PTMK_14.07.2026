using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TicketRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        await _dbContext.Tickets.AddAsync(ticket, cancellationToken);
    }

    public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tickets.AnyAsync(t => t.Id == id, cancellationToken);
    }
}