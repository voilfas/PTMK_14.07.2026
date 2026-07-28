namespace TicketService.Application.DTOs.EmployeeDTOs;

public record EmployeeListItemForTicketDto(
    Guid EmployeeId,
    string FullName,
    string DepartmentName,
    string PositionName);