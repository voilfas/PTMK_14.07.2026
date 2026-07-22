namespace TicketService.Application.DTOs;

public sealed record PositionDto(
    string Name,
    bool IsActive);