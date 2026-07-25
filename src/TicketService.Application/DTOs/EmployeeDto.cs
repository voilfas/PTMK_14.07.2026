namespace TicketService.Application.DTOs;

public sealed record EmployeeDto(
    string FirstName,
    string LastName,
    string Surname,
    Guid DepartmentId,
    string DepartmentName,
    Guid PositionId,
    string PositionName
    );