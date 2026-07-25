namespace TicketService.Application.DTOs;

public sealed record EmployeeListItemDto(
    Guid Id,
    string FullName,
    string DepartmentName,
    string PositionName
    );