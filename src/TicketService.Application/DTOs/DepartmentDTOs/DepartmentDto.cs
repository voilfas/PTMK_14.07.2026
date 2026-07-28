namespace TicketService.Application.DTOs.DepartmentDTOs;

public sealed record DepartmentDto(
    string Name,
    string Code,
    bool IsActive
    );