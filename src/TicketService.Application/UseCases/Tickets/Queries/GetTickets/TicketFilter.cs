using TicketService.Application.Common;
using TicketService.Domain.Enums;
using TicketService.Domain.ValueObjects;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTickets;

public record TicketFilter(
    Guid? AuthorId,
    Guid? ExecutorId,
    TicketStatus? Status,
    TicketType? Type,
    DateTime? CreatedFrom,
    DateTime? CreatedTo,
    DateTime? DeadlineFrom,
    DateTime? DeadlineTo,
    bool? IsOverdue,
    string? DepartmentCode
    ) : PageQuery;