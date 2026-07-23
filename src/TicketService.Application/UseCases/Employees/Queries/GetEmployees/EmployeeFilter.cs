using TicketService.Application.Common;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployees;

public record EmployeeFilter(
    Guid? DepartmentId,
    Guid? PositionId,
    string? Search
    ) : PageQuery;