namespace TicketService.Application.DTOs;

public record EmployeeListItemForTicketDto(
    Guid EmployeeId,
    string FullName,
    string DepartmentName,
    string PositionName);