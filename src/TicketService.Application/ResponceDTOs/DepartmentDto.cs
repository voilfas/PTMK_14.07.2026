namespace TicketService.Application.ResponceDTOs;

public sealed record DepartmentDto(
    string Name,
    string Code,
    bool IsActive
    );