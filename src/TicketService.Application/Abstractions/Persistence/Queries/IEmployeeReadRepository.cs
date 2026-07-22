using TicketService.Application.Common;
using TicketService.Application.DTOs;
using TicketService.Application.UseCases.Employees.Queries.GetEmployees;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IEmployeeReadRepository
{
    Task<PageResult<EmployeeDto>> GetAllAsync(
        EmployeeFilter filter,
        CancellationToken cancellationToken = default);
}