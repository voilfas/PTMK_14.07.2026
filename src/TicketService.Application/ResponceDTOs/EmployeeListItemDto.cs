namespace TicketService.Application.ResponceDTOs;

public sealed record EmployeeListItemDto(
    Guid Id,
    string FullName,
    string DepartmentName,
    string PositionName
    );