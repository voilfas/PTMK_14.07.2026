using TicketService.Domain.Enums;

namespace TicketService.Application.DTOs;

public sealed record TicketStatusReportDto(
    TicketStatus TicketStatus,
    int Count);