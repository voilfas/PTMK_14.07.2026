using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.EmployeeDTOs;
using TicketService.Application.Features.Employees.Queries.GetEmployees;
using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IEmployeeReadRepository
{
    Task<PageResult<EmployeeListItemDto>> GetAllAsync(
        EmployeeFilter filter,
        CancellationToken cancellationToken);

    Task<EmployeeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}