using TicketService.Application.Common;
using TicketService.Domain.Enums;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTickets;

public record TicketFilter(
    Guid? AuthorId,
    Guid? ExecutorId,
    TicketStatus? Status,
    TicketType? Type,
    DateTime? CreatedFrom,
    DateTime? CreatedTo,
    DateTime? DeadlineFrom,
    DateTime? DeadlineTo
    ) : PageQuery;