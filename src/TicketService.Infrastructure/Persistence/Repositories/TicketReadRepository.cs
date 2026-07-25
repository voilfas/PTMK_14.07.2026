using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;
using TicketService.Application.UseCases.Tickets.Queries.GetTickets;
using TicketService.Domain.ValueObjects;

namespace TicketService.Infrastructure.Persistence.Repositories;

public class TicketReadRepository : ITicketReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TicketReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TicketDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TicketDto(
                t.Id,
                t.TicketNumber.Number,
                t.Status.ToString(),
                t.AuthorId,
                t.Author.FullName.ToString(),
                t.Description,
                t.Type.ToString(),
                t.CreatedAt,
                t.Deadline,
                t.Executors
                    .Select(te => new EmployeeListItemForTicketDto(
                        te.Employee.Id,
                        te.Employee.FullName.ToString(),
                        te.Employee.Department.Name,
                        te.Employee.Position.Name))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<PageResult<TicketListItemDto>> GetAllAsync(TicketFilter filter, CancellationToken cancellationToken)
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
        
        if (filter.IsOverdue == true)
            query = query.Where(t => t.Deadline < DateTime.UtcNow);
        else if (filter.IsOverdue == false)
            query = query.Where(t => t.Deadline >= DateTime.UtcNow);

        if (!string.IsNullOrWhiteSpace(filter.DepartmentCode))
        {
            var departmentCode = CodeDepartment.FromDatabase(filter.DepartmentCode);

            query = query.Where(t =>
                t.Executors.Any(te =>
                    te.Employee.Department.Code == departmentCode));
        }
        
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var tickets = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TicketListItemDto(
                t.Id,
                t.TicketNumber.Number,
                t.Description,
                t.Status,
                t.Deadline,
                t.CreatedAt))
            .ToListAsync(cancellationToken);
        
        return new PageResult<TicketListItemDto>
        {
            Items = tickets,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }
}