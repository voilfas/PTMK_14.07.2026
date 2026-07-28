using Microsoft.EntityFrameworkCore;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.EmployeeDTOs;
using TicketService.Application.DTOs.TicketDTOs;
using TicketService.Application.Features.Tickets.Queries.GetTickets;
using TicketService.Domain.Enums;
using TicketService.Domain.ValueObjects;

namespace TicketService.Infrastructure.Persistence.Repositories;

public class TicketReadRepository : ITicketReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TicketReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TicketDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
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

    public async Task<PageResult<TicketListItemDto>> GetAllAsync(
        TicketFilter filter,
        CancellationToken cancellationToken)
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
        
        
        var totalCount = await query.CountAsync(
            cancellationToken);
        
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

    public async Task<IReadOnlyCollection<TicketStatusReportDto>> GetStatusReportAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .GroupBy(t => t.Status)
            .Select(t => new TicketStatusReportDto(
                t.Key,
                t.Count()))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetAmountOverdueAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .Where(t => t.Deadline < DateTime.UtcNow)
            .CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TicketCompletedAmountExecutorDto>> GetCompletedAmountAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .Where(t => t.Status == TicketStatus.Completed)
            .SelectMany(t => t.Executors)
            .GroupBy(te => new
            {
                te.EmployeeId,
                te.Employee.FullName.FirstName,
                te.Employee.FullName.LastName,
                te.Employee.FullName.Surname
            })
            .OrderByDescending(g => g.Count())
            .Select(g => new TicketCompletedAmountExecutorDto(
                g.Key.EmployeeId,
                $"{g.Key.LastName} {g.Key.FirstName} {g.Key.Surname}",
                g.Count()))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TicketListItemDto>> GetRecentAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Take(30)
            .Select(t => new TicketListItemDto(
                t.Id,
                t.TicketNumber.Number,
                t.Description,
                t.Status,
                t.Deadline,
                t.CreatedAt
                ))
            .ToListAsync(cancellationToken);
    }
}