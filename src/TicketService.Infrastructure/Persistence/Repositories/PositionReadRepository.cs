using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Infrastructure.Persistence.Repositories;

public sealed class PositionReadRepository : IPositionReadRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public PositionReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PageResult<PositionDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Positions.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        
        var positions = await query
            .OrderBy(d => d.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PositionDto(
                p.Name,
                p.IsActive))
            .ToListAsync(cancellationToken);
        
        return new PageResult<PositionDto>()
        {
            Items =  positions,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}