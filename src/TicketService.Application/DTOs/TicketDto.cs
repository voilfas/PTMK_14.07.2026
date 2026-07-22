using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs;

public sealed record TicketDto(
    Guid Id,
    string Number,
    string Description,
    TicketStatus Status,
    DateTime Deadline);