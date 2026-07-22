namespace TicketService.Application.DTOs;

public sealed record DepartmentDto(
    string Name,
    string Code,
    bool IsActive
    );