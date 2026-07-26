using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;
using TicketService.Application.UseCases.Employees.Queries.GetEmployees;
using TicketService.Domain.Entities;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface IEmployeeReadRepository
{
    Task<PageResult<EmployeeListItemDto>> GetAllAsync(
        EmployeeFilter filter,
        CancellationToken cancellationToken);

    Task<EmployeeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}