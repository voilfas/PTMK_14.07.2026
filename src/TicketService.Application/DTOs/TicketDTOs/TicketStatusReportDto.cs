using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs.TicketDTOs;

public sealed record TicketStatusReportDto(
    TicketStatus TicketStatus,
    int Count);