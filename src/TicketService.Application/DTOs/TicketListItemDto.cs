using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs;

public sealed record TicketListItemDto(
    string Number,
    string Description,
    TicketStatus Status,
    DateTime Deadline,
    DateTime CreatedAt);