using TicketService.Domain.Enums;

namespace TicketService.Application.ResponceDTOs;

public sealed record TicketStatusReportDto(
    TicketStatus TicketStatus,
    int Count);