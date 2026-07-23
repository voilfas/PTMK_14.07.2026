using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;
using TicketService.Application.UseCases.Tickets.Queries.GetTickets;

namespace TicketService.Infrastructure.Persistence.Repositories;

public class TicketReadRepository : ITicketReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TicketReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TicketDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var ticket = await _dbContext.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

         if (ticket == null)
             return null;
         
         var ticketDto = new TicketDto(
             ticket.Id,
             ticket.TicketNumber.Number,
             ticket.Description,
             ticket.Status,
             ticket.Deadline);
         
         return ticketDto;
    }

    public async Task<PageResult<TicketDto>> GetAllAsync(TicketFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Tickets.AsNoTracking();

        if (filter.AuthorId is not null)
            query = query.Where(t => t.AuthorId == filter.AuthorId);

        if (filter.ExecutorId is not null)
            query = query.Where(t => t.Executors.Any(e => e.EmployeeId == filter.ExecutorId));

        if (filter.Status is not null)
            query = query.Where(t => t.Status == filter.Status);
        
        if (filter.Type is not null)
            query = query.Where(t => t.Type == filter.Type);
        
        if (filter.CreatedFrom is not null)
            query = query.Where(t => t.CreatedAt >= filter.CreatedFrom);
        
        if (filter.CreatedTo is not null)
            query = query.Where(t => t.CreatedAt <= filter.CreatedTo);
        
        if (filter.DeadlineFrom is not null)
            query = query.Where(t => t.Deadline >= filter.DeadlineFrom);
        
        if (filter.DeadlineTo is not null)
            query = query.Where(t => t.Deadline <= filter.DeadlineTo);
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var tickets = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TicketDto(
                t.Id,
                t.TicketNumber.Number,
                t.Description,
                t.Status,
                t.Deadline))
            .ToListAsync(cancellationToken);
        
        return new PageResult<TicketDto>
        {
            Items = tickets,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<PageResult<TicketDto>> GetOverdueAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Tickets
            .AsNoTracking()
            .Where(t => t.Deadline <= DateTime.UtcNow);

        var totalCount = await query.CountAsync(cancellationToken);
        
        var tickets = await query
            .OrderBy(t => t.Deadline)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TicketDto(
                t.Id,
                t.TicketNumber.Number,
                t.Description,
                t.Status,
                t.Deadline))
            .ToListAsync(cancellationToken);

        return new PageResult<TicketDto>
        {
            Items = tickets,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}