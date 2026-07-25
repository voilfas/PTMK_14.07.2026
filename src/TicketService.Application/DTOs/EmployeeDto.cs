namespace TicketService.Application.DTOs;

public sealed record EmployeeDto(
    Guid EmployeeId,
    string FirstName,
    string LastName,
    string Surname,
    Guid DepartmentId,
    string DepartmentName,
    Guid PositionId,
    string PositionName
    );