using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs;

public sealed record TicketListItemDto(
    Guid Id,
    string Number,
    string Description,
    TicketStatus Status,
    DateTime Deadline,
    DateTime CreatedAt);