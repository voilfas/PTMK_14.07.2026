using TicketService.Application.DTOs.EmployeeDTOs;

namespace TicketService.Application.DTOs.TicketDTOs;

public record TicketDto(
    Guid TicketId,
    string TicketNumber,
    string Status,
    Guid AuthorId,
    string AuthorName,
    string Description,
    string Type,
    DateTime CreatedAt,
    DateTime Deadline,
    IReadOnlyCollection<EmployeeListItemForTicketDto> Executors);