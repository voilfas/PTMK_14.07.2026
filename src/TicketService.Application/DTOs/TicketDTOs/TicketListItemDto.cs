using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs.TicketDTOs;

public sealed record TicketListItemDto(
    Guid Id,
    string Number,
    string Description,
    TicketStatus Status,
    DateTime Deadline,
    DateTime CreatedAt);