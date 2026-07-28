using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;

namespace TicketService.Application.Features.Employees.Queries.GetEmployees;

public record EmployeeFilter(
    Guid? DepartmentId,
    Guid? PositionId,
    string? Search
    ) : PageQuery;