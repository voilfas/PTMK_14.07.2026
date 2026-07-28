using TicketService.Application.Common.Pagination;
using TicketService.Domain.Enums;

namespace TicketService.Application.Features.Tickets.Queries.GetTickets;

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