namespace TicketService.Application.DTOs;

public sealed record EmployeeListItemDto(
    string FullName,
    string DepartmentName,
    string PositionName
    );