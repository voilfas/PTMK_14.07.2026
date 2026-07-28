namespace TicketService.Application.DTOs.PositionDTOs;

public sealed record PositionDto(
    string Name,
    bool IsActive);