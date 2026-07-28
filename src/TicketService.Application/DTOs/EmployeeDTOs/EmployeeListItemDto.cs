namespace TicketService.Application.DTOs.EmployeeDTOs;

public sealed record EmployeeListItemDto(
    Guid Id,
    string FullName,
    string DepartmentName,
    string PositionName
    );